using System.Net.Http;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Client.Clients;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestManager
    {
        private readonly HttpClient _httpClient;

        public readonly PowerDmsRestApiClient PowerDmsRestApiClient;

        private readonly IAuthenticationTokenProvider _authenticationTokenProvider;

        public HttpRequestManager(HttpClient httpClient, IAuthenticationTokenProvider authenticationTokenProvider)
        {
            _httpClient = httpClient;
            PowerDmsRestApiClient = new PowerDmsRestApiClient(httpClient);
            _authenticationTokenProvider = authenticationTokenProvider;
        }

        public virtual async Task<HttpResponseMessage> SendAsync<T>(HttpRequestBuilder<T> httpRequestBuilder)
        {
            // virtual to facilitate faking during unit testing
            await AddAuthentication(httpRequestBuilder);
            return await _httpClient.SendAsync(httpRequestBuilder.HttpRequestMessage);
        }

        private async Task<HttpRequestBuilder<T>> AddAuthentication<T>(
            HttpRequestBuilder<T> httpRequestBuilder)
        {
            httpRequestBuilder.HttpRequestMessage
                .AddAccessToken(await _authenticationTokenProvider.GetAccessToken());

            return httpRequestBuilder;
        }

        public async Task<TResponse> GetSuccessfulResponse<TResponse>(
            HttpRequestBuilder<TResponse> httpRequestBuilder)
        {
            return await SendAsync(httpRequestBuilder)
                .AwaitGetSuccessfulResponse<TResponse>();
        }

        public async Task<ErrorDto> GetErrorResponse<TResponse, TError>(
            HttpRequestBuilder<TResponse> httpRequestBuilder)
        {
            return await SendAsync(httpRequestBuilder)
                .AwaitGetErrorResponse<TError>();
        }
    }
}