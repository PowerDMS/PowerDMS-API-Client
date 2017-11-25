using System.Net.Http;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Dto;

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
            return _HttpClient.SendAsync(GetGroupRequest(groupId));
        }

        public HttpRequestMessage GetGroupRequest(string groupId)
        {
            return new HttpRequestMessage(
                HttpMethod.Get, 
                $"{RestApiRoutes.Groups}/{groupId}");
        }

        public HttpRequestBuilder<GroupDto> GetGroupRequestBuilder(string groupId)
        {
            return new HttpRequestBuilder<GroupDto>(
                GetGroupRequest(groupId));
        }
    }
}