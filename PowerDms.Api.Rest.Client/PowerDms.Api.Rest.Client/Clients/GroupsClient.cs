using System.Net.Http;
using System.Threading.Tasks;

namespace PowerDms.Api.Rest.Client.Clients
{
    using Dto;

    public class GroupsClient
    {
        /// <summary>
        /// I think this is going away, Manager will handle the sending
        /// </summary>
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

        public HttpRequestMessage PostGroupRequest(GroupDto groupDto)
        {
            return new HttpRequestMessage(
                HttpMethod.Post,
                RestApiRoutes.Groups);
        }
        public HttpRequestBuilder<GroupDto> PostGroupRequestBuilder(GroupDto groupDto)
        {
            return new HttpRequestBuilder<GroupDto>(PostGroupRequest(groupDto))
                .AddJsonBody(groupDto);
        }
    }
}