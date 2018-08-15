using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    using System;

    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> GetContent<T>(this HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
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
            var t = await httpResponseMessage
                .EnsureSuccessStatusCode()
                .GetContent<ServiceResponseDto<TResponse>>();

            return t.Data;
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