using System.Net.Http;
using System.Net.Http.Headers;

namespace PowerDms.Api.Rest.Client
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddAccessToken(
            this HttpRequestMessage httpRequestMessage,
            string oauthAccesstoken)
        {
            httpRequestMessage.Headers.Authorization = 
                new AuthenticationHeaderValue("Bearer", oauthAccesstoken);

            return httpRequestMessage;
        }
    }
}