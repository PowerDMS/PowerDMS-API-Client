using System.Net.Http;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Client.Clients;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestManager
    {
        private readonly HttpClient _HttpClient;

        public readonly PowerDmsRestApiClient PowerDmsRestApiClient;

        private readonly IAuthenticationTokenProvider _AuthenticationTokenProvider;

        public HttpRequestManager(
            HttpClient httpClient,
            AuthenticationTokenProvider authenticationTokenProvider)
        {
            _HttpClient = httpClient;
            PowerDmsRestApiClient = new PowerDmsRestApiClient(httpClient);
            _AuthenticationTokenProvider = authenticationTokenProvider;
        }

        public async Task<HttpResponseMessage> SendAsync<T>(HttpRequestBuilder<T> httpRequestBuilder)
        {
            await AddAuthentication(httpRequestBuilder);
            return await _HttpClient.SendAsync(httpRequestBuilder.HttpRequestMessage);
        }

        private async Task<HttpRequestBuilder<T>> AddAuthentication<T>(
            HttpRequestBuilder<T> httpRequestBuilder)
        {
            if (httpRequestBuilder.Credentials != null)
            {
                httpRequestBuilder.HttpRequestMessage
                    .AddAccessToken(
                        await _AuthenticationTokenProvider.GetAccessToken(httpRequestBuilder.Credentials));

            }

            return httpRequestBuilder;
        }

        public async Task<TResponse> GetSuccessfulResponse<TResponse>(
            HttpRequestBuilder<TResponse> httpRequestBuilder)
        {
            return await SendAsync(httpRequestBuilder)
                .AwaitGetSuccessfulResponse<TResponse>();
        }

        public async Task<TError> GetErrorResponse<TResponse, TError>(
            HttpRequestBuilder<TResponse> httpRequestBuilder)
        {
            return await SendAsync(httpRequestBuilder)
                .AwaitGetErrorResponse<TError>();
        }
    }
}