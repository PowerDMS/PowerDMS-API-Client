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

            var requestManager =
                new HttpRequestManagerBuilder()
                    .SetApiHost(configuration["Host"])      // only needed if not using public PowerDMS API
                    .SetOAuthHost(configuration["Host"])    // only needed if not using public PowerDMS API
                    .Build(credentials);

            var newGroup = new GroupDto
            {
                Name = "ApiCreated" + Guid.NewGuid()
            };

            var createdGroup = await NewGroup(requestManager, newGroup);

            Console.WriteLine($"  ID: {createdGroup.Id}");
            Console.WriteLine($"  Name: {createdGroup.Name}");

            var getItAgain = await GetGroup(requestManager, createdGroup.Id);

            var getBogusOne = await GetGroup(requestManager, "0");

            Console.WriteLine("-- press any key to exit --");
            Console.ReadKey();
        }

        private static async Task<GroupDto> GetGroup(HttpRequestManager requestManager, string groupId)
        {
            // I don't like the fact credentials need to be set on each builder, why not in the manager?
            var requestBuilder = requestManager.PowerDmsRestApiClient.Groups
                .GetGroupRequestBuilder(groupId);

            var result = await requestManager
                .SendAsync(requestBuilder)
                .AwaitGetServiceResponse<GroupDto>();

            if (!result.IsSuccessful)
            {
                Console.WriteLine($"-- Error! Code: {result.Error.Code}, Message: {result.Error.Messages?.FirstOrDefault()} --");
                return null;
            }

            return result.Data;
        }

        private static async Task<GroupDto> NewGroup(HttpRequestManager requestManager, GroupDto groupDto)
        {
            var requestBuilder = requestManager.PowerDmsRestApiClient.Groups
                .PostGroupRequestBuilder(groupDto);

            var result = await requestManager
                .SendAsync(requestBuilder)
                .AwaitGetServiceResponse<GroupDto>();

            if (!result.IsSuccessful)
            {
                Console.WriteLine($"-- Error! Code: {result.Error.Code}, Message: {result.Error.Messages?.FirstOrDefault()} --");
                return null;
            }

            return result.Data;
        }
    }
}
