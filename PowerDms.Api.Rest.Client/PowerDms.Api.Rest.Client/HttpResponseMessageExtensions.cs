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
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static async Task<T> AwaitGetSuccessfulResponse<T>(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            return await (await httpResponseMessageTask)
                .GetSuccessfulResponse<T>();
        }

        public static async Task<T> AwaitGetErrorRespones<T>(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            return await (await httpResponseMessageTask)
                .GetErrorResponse<T>();
        }

        public static async Task<T> GetSuccessfulResponse<T>(this HttpResponseMessage httpResponseMessage)
        {
            return await httpResponseMessage
                .EnsureSuccessStatusCode()
                .GetContent<T>();
        }

        public static async Task<T> GetErrorResponse<T>(this HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            return await httpResponseMessage
                .GetContent<T>();
        }
    }
}