using System.Net.Http;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Client.Clients;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestBuilder
    {
        private readonly PowerDmsRestApiClient _PowerDmsRestApiClient;

        private HttpRequestMessage _HttpRequestMessage;

        private readonly HttpClient _HttpClient;

        private readonly IAuthenticationTokenProvider _AuthenticationTokenProvider;

        public HttpRequestBuilder(
            HttpRequestMessage httpRequestMessage,
            HttpClient httpClient,
            IAuthenticationTokenProvider authenticationTokenProvider = null)
        {
            _HttpRequestMessage = httpRequestMessage;
            _HttpClient = httpClient;
            _AuthenticationTokenProvider = authenticationTokenProvider;
        }

        public async Task<HttpRequestBuilder> AuthenticateWith(
            Credentials credentials)
        {
            if (_AuthenticationTokenProvider == null)
            {
                throw new NoAuthenticationProviderException();
            }

            _HttpRequestMessage
                .AddAccessToken(
                    await _AuthenticationTokenProvider.GetAccessToken(credentials));

            return this;
        }

        public Task<HttpResponseMessage> SendAsync()
        {
            return _HttpClient.SendAsync(_HttpRequestMessage);
        }
    }
}