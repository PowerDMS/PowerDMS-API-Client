using System.Net.Http;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Client.Clients;

namespace PowerDms.Api.Rest.Client
{
    using System;

    public class HttpRequestBuilder<TResponse>
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

        public async Task<HttpRequestBuilder<TResponse>> AuthenticateWith(
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