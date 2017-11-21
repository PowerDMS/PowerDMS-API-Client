using System.Net.Http;
using System.Threading.Tasks;

namespace PowerDms.Api.Rest.Client
{
    public static class HttpRequestBuilderExtensions
    {
        public static async Task<HttpResponseMessage> AwaitSendAsync<TResponse>(
            this Task<HttpRequestBuilder<TResponse>> httpRequestBuilderTask)
        {
            return await (await httpRequestBuilderTask).SendAsync();
        }

        public static async Task<TResponse> AwaitGetSuccessfulResponse<TResponse>(this Task<HttpRequestBuilder<TResponse>> httpRequestBuilderTask)
        {
            return await httpRequestBuilderTask
                .AwaitSendAsync()
                .AwaitGetSuccessfulResponse<TResponse>();
        }

        public static async Task<TError> AwaitGetErrorResponse<TResponse, TError>(this Task<HttpRequestBuilder<TResponse>> httpRequestBuilderTask)
        {
            return await httpRequestBuilderTask
                .AwaitSendAsync()
                .AwaitGetErrorResponse<TError>();
        }
    }
}