using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public static class HttpResponseMessageExtensions
    {
        // TODO: replace this when .net std 2.1 is out:
        // https://github.com/dotnet/corefx/issues/26201
        private static string _applicationJson = "application/json";

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

        public static async Task<ErrorDto> AwaitGetErrorResponse<TError>(this Task<HttpResponseMessage> httpResponseMessageTask)
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

        public static async Task<ErrorDto> GetErrorResponse<TError>(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            if (httpResponseMessage.Content.Headers.ContentType.MediaType == _applicationJson)
            {
                var response = await httpResponseMessage
                    .GetContent<ServiceResponseDto<TError>>();

                if (response == null)
                {
                    return new ErrorDto
                    {
                        Code = httpResponseMessage.StatusCode.ToString(),
                        HttpStatusCode = (int)httpResponseMessage.StatusCode
                    };
                }
                return response.Error;
            }
            else
            {
                var response = await httpResponseMessage.Content.ReadAsStringAsync();

                return new ErrorDto
                {
                    Code = httpResponseMessage.StatusCode.ToString(),
                    HttpStatusCode = (int)httpResponseMessage.StatusCode,
                    Messages = response != null ? new []{response} : null
                };
            }
        }

        public static async Task<ServiceResponseDto<T>> GetServiceResponse<T>(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.Content.Headers.ContentType.MediaType == _applicationJson)
            {
                return await httpResponseMessage
                    .GetContent<ServiceResponseDto<T>>();
            }

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var response = await httpResponseMessage.Content.ReadAsStringAsync();

                return new ServiceResponseDto<T>
                {
                    Error = new ErrorDto
                    {
                        Code = httpResponseMessage.StatusCode.ToString(),
                        HttpStatusCode = (int) httpResponseMessage.StatusCode,
                        Messages = response != null ? new[] {response} : null
                    }
                };
            }

            return new ServiceResponseDto<T>
            {
                Data = default(T)
            };
        }

        public static async Task<ServiceResponseDto> GetServiceResponse(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return new ServiceResponseDto();
            }

            if (httpResponseMessage.Content.Headers.ContentType.MediaType == _applicationJson)
            {
                return await httpResponseMessage
                    .GetContent<ServiceResponseDto>();
            }

            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            return new ServiceResponseDto
            {
                Error = new ErrorDto
                {
                    Code = httpResponseMessage.StatusCode.ToString(),
                    HttpStatusCode = (int) httpResponseMessage.StatusCode,
                    Messages = response != null ? new[] {response} : null
                }
            };
        }

        public static async Task<ServiceResponseDto<T>> AwaitGetServiceResponse<T>(
            this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            return await (await httpResponseMessageTask).GetServiceResponse<T>();
        }

        public static async Task<ServiceResponseDto> AwaitGetServiceResponse(
            this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            return await (await httpResponseMessageTask).GetServiceResponse();
        }
    }
}