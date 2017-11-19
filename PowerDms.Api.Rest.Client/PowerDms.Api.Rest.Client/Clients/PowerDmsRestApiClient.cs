using System.Net.Http;

namespace PowerDms.Api.Rest.Client.Clients
{
    public class PowerDmsRestApiClient
    {
        private readonly HttpClient _HttpClient;

        public readonly GroupsClient Groups;

        public readonly OAuthClient OAuth;

        public PowerDmsRestApiClient(
            HttpClient httpClient)
        {
            _HttpClient = httpClient;

            Groups = new GroupsClient(_HttpClient);
            OAuth = new OAuthClient(_HttpClient);
        }
    }
}