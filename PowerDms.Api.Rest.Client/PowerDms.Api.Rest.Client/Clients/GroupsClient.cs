using System.Net.Http;
using System.Threading.Tasks;

namespace PowerDms.Api.Rest.Client.Clients
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

        public HttpRequestMessage GetGroupRequest(string groupId)
        {
            return new HttpRequestMessage(
                HttpMethod.Get, 
                $"{RestApiRoutes.Groups}/{groupId}");
        }

        public HttpRequestBuilder GetGroupRequestBuilder(string groupId)
        {
            return new HttpRequestBuilder(
                GetGroupRequest(groupId),
                _HttpClient);
        }
    }
}