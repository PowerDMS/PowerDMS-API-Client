using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestBuilder
    {
        private readonly PowerDmsRestApiClient _PowerDmsRestApiClient;

        private HttpRequestMessage _HttpRequestMessage;

        private readonly HttpClient _HttpClient;

        public HttpRequestBuilder(
            HttpRequestMessage httpRequestMessage,
            HttpClient httpClient)
        {
            _HttpRequestMessage = httpRequestMessage;
            _HttpClient = httpClient;
        }

        public async Task<HttpRequestBuilder> AuthenticateWith(
            Credentials credentials)
        {
            _HttpRequestMessage
                .AddAccessToken(
                    await (await _PowerDmsRestApiClient.OAuth.GetAccessToken(
                        credentials.Username,
                        credentials.Password,
                        credentials.SiteKey,
                        credentials.ClientSecret
                    )).GetContent<string>());

            return this;
        }

        public Task<HttpResponseMessage> SendAsync()
        {
            return _HttpClient.SendAsync(_HttpRequestMessage);
        }
    }
}