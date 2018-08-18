using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PowerDms.Api.Rest.Client;
using PowerDms.Api.Rest.Dto;

namespace DemoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // because there is no async Main until c# 7
            try
            {
                DoDemo().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task DoDemo()
        {
            // do not check in credentials, copy the sample appsettings.json file here
            var appsettingsPath = "c:\\temp";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(appsettingsPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var credentials =
                new Credentials
                {
                    SiteKey = configuration["Credentials:SiteKey"],
                    Username = configuration["Credentials:Username"],
                    Password = configuration["Credentials:Password"],
                    ClientSecret = configuration["Credentials:ClientSecret"]
                };

            var httpClient = PowerDmsHttpClientFactory.CreateHttpClient(configuration["Host"], ApiVersion.Version1);
            var requestManager = new HttpRequestManagerFactory().CreateInstance(httpClient);

            Console.WriteLine("Create Group - please delete from Site");

            var newGroup = new GroupDto
            {
                Name = "ApiCreated" + Guid.NewGuid()
            };

            var createdGroup = await NewGroup(requestManager, credentials, newGroup);

            Console.WriteLine($"  ID: {createdGroup.Id}");
            Console.WriteLine($"  Name: {createdGroup.Name}");

            var getItAgain = await GetGroup(requestManager, credentials, createdGroup.Id);

            var getBogusOne = await GetGroup(requestManager, credentials, "0");

            Console.WriteLine("-- press any key to exit --");
            Console.ReadKey();
        }

        private static async Task<GroupDto> GetGroup(HttpRequestManager requestManager, Credentials credentials, string groupId)
        {
            // I don't like the fact credentials need to be set on each builder, why not in the manager?
            var requestBuilder = requestManager.PowerDmsRestApiClient.Groups
                .GetGroupRequestBuilder(groupId)
                .AuthenticateWith(credentials);

            var result = await requestManager.SendAsync(requestBuilder);

            if (!result.IsSuccessStatusCode)
            {
                // CR note: why would I need to pass a <T> for errors?
                var error = await result.GetErrorResponse<GroupDto>();
                Console.WriteLine($"-- Error! Code: {error.Code}, Message: {error.Messages?.FirstOrDefault()} --");
                return null;
            }

            return await result.GetSuccessfulResponse<GroupDto>();
        }

        private static async Task<GroupDto> NewGroup(HttpRequestManager requestManager, Credentials credentials, GroupDto groupDto)
        {
            var requestBuilder = requestManager.PowerDmsRestApiClient.Groups
                .PostGroupRequestBuilder(groupDto)
                .AuthenticateWith(credentials);

            var result = await requestManager.SendAsync(requestBuilder);

            if (!result.IsSuccessStatusCode)
            {
                // CR note: why would I need to pass a <T> for errors?
                var error = await result.GetErrorResponse<GroupDto>();
                Console.WriteLine($"-- Error! Code: {error.Code}, Message: {error.Messages?.FirstOrDefault()} --");
                return null;
            }

            return await result
                .GetSuccessfulResponse<GroupDto>();
        }
    }
}
