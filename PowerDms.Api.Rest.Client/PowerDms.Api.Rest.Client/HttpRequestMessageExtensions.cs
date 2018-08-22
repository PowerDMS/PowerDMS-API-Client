using System.Net.Http;
using System.Net.Http.Headers;

namespace PowerDms.Api.Rest.Client
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddAccessToken(
            this HttpRequestMessage httpRequestMessage,
            AuthenticationHeaderValue httpAuthorization)
        {
            httpRequestMessage.Headers.Authorization = httpAuthorization;

            return httpRequestMessage;
        }
    }
}