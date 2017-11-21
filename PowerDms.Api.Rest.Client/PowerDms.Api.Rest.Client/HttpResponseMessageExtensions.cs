using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerDms.Api.Rest.Client
{
    using System;

    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> GetContent<T>(this HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject("");
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static async Task<TResponse> AwaitGetSuccessfulResponse<TResponse>(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            return await (await httpResponseMessageTask)
                .GetSuccessfulResponse<TResponse>();
        }

        public static async Task<TError> AwaitGetErrorResponse<TError>(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            return await (await httpResponseMessageTask)
                .GetErrorResponse<TError>();
        }

        public static async Task<TResponse> GetSuccessfulResponse<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            return await httpResponseMessage
                .EnsureSuccessStatusCode()
                .GetContent<TResponse>();
        }

        public static async Task<TError> GetErrorResponse<TError>(this HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            return await httpResponseMessage
                .GetContent<TError>();
        }
    }
}