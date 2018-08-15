using System;
using Microsoft.Extensions.Configuration;
using PowerDms.Api.Rest.Client;
using PowerDms.Api.Rest.Dto;

namespace DemoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // do not check in credentials, copy the sample appsettings.json file here
            var appsettingsPath = "c:\\temp";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(appsettingsPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var httpClient = PowerDmsHttpClientFactory.CreateHttpClient(configuration["Host"], ApiVersion.Version1);
            var requestManager = new HttpRequestManager(httpClient);
            var requestBuilder =
                    requestManager.PowerDmsRestApiClient.Groups
                        .GetGroupRequestBuilder("1717316")
                        .AuthenticateWith(new Credentials
                        {
                            SiteKey = configuration["Credentials:SiteKey"],
                            Username = configuration["Credentials:Username"],
                            Password = configuration["Credentials:Password"],
                            ClientSecret = configuration["Credentials:ClientSecret"]
                        });

            Console.WriteLine("Get Group");

            var response = requestManager.SendAsync(requestBuilder).AwaitGetSuccessfulResponse<GroupDto>().Result;

            Console.WriteLine($"  Name: {response.Name}");

            Console.WriteLine("-- press any key to exit --");
            Console.ReadKey();
        }
    }
}
