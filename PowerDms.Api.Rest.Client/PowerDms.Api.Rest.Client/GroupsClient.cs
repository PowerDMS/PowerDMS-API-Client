using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PowerDms.Api.Rest.Client
{
    public class GroupsClient
    {
        private readonly HttpClient _HttpClient;

        public GroupsClient(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public Task<HttpResponseMessage> GetGroup(string groupId)
        {
            return _HttpClient.GetAsync($"{RestApiRoutes.Groups}/{groupId}");
        }
    }
}