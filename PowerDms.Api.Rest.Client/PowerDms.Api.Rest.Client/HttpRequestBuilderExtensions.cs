using System.Net.Http;
using System.Threading.Tasks;

namespace PowerDms.Api.Rest.Client
{
    public static class HttpRequestBuilderExtensions
    {
        public static async Task<HttpResponseMessage> AwaitSendAsync(
            this Task<HttpRequestBuilder> httpRequestBuilderTask)
        {
            return await (await httpRequestBuilderTask).SendAsync();
        }

        public static async Task<T> AwaitGetSuccessfulResponse<T>(this Task<HttpRequestBuilder> httpRequestBuilderTask)
        {
            return await httpRequestBuilderTask
                .AwaitSendAsync()
                .AwaitGetSuccessfulResponse<T>();
        }
    }
}