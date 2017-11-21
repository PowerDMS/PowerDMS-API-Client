namespace PowerDms.Api.Rest.Client.Clients
{
    using System.Net.Http;

    public class UsersClient
    {
        private readonly HttpClient _HttpClient;

        public UsersClient(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public HttpRequestMessage PostUserRequest(UserDetailsDto userDetailsDto)
        {
            return new HttpRequestMessage(
                HttpMethod.Post,
                RestApiRoutes.Users);
        }
    }
}