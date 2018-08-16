using System.Net.Http;
using System.Net.Http.Headers;

namespace PowerDms.Api.Rest.Client
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddAccessToken(
            this HttpRequestMessage httpRequestMessage,
            HttpAuthorization httpAuthorization)
        {
            httpRequestMessage.Headers.Authorization = 
                new AuthenticationHeaderValue(httpAuthorization.Type, httpAuthorization.Credentials);

            return httpRequestMessage;
        }
    }
}