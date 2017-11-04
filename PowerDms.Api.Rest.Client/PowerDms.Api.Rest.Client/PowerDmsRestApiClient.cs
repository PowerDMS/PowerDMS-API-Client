using System;
using System.Net.Http;

namespace PowerDms.Api.Rest.Client
{
    public class PowerDmsRestApiClient
    {
        private readonly HttpClient _HttpClient;

        public readonly GroupsClient Groups;

        public PowerDmsRestApiClient(ApiVersion version) 
            : this(RestApiRoutes.PowerDmsRestApiDomain, version)
        {
        }

        public PowerDmsRestApiClient(
            string powerDmsRestApiDomain,
            ApiVersion version)
        {
            version = version ?? ApiVersion.Version1;

            _HttpClient = new HttpClient
            {
                BaseAddress = new Uri($"{powerDmsRestApiDomain}/{version}")
            };

            Groups = new GroupsClient(_HttpClient);
        }
    }
}