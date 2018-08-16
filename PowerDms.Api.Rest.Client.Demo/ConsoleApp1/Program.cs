using System;
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
            var requestManager = new HttpRequestManager(httpClient);

            Console.WriteLine("Get Group");

            var group = await GetGroup(requestManager, credentials, "1717316");

            Console.WriteLine($"  Name: {group.Name}");

            group.Name += "+";
            var newGroup = await NewGroup(requestManager, credentials, group);

            Console.WriteLine($"  Name: {newGroup.Name}");

            Console.WriteLine("-- press any key to exit --");
            Console.ReadKey();
        }

        private static async Task<GroupDto> GetGroup(HttpRequestManager requestManager, Credentials credentials, string groupId)
        {
            // I don't like the fact credentials need to be set on each builder, why not in the manager?
            var requestBuilder = requestManager.PowerDmsRestApiClient.Groups
                .GetGroupRequestBuilder(groupId)
                .AuthenticateWith(credentials);

            return await requestManager.SendAsync(requestBuilder)
                .AwaitGetSuccessfulResponse<GroupDto>();
        }

        private static async Task<GroupDto> NewGroup(HttpRequestManager requestManager, Credentials credentials, GroupDto groupDto)
        {
            var requestBuilder = requestManager.PowerDmsRestApiClient.Groups
                .PostGroupRequestBuilder(groupDto)
                .AuthenticateWith(credentials);

            return await requestManager.SendAsync(requestBuilder)
                .AwaitGetSuccessfulResponse<GroupDto>();
        }
    }
}
