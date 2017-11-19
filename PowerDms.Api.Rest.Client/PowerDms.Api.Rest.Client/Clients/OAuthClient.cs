using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PowerDms.Api.Rest.Client.Clients
{
    public class OAuthClient
    {
        private readonly HttpClient _HttpClient;

        public OAuthClient(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public Task<HttpResponseMessage> GetAccessToken(
            string username,
            string password,
            string siteKey,
            string oauthClientSecret)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            httpRequestMessage.Headers.Authorization = 
                new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(oauthClientSecret)));

            var content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("scope", "openid profile tnt_profile privileges"),
                new KeyValuePair<string, string>("acr_values", $"tenant:{siteKey}")
            };

            httpRequestMessage.Content = new FormUrlEncodedContent(content);

            return _HttpClient.SendAsync(httpRequestMessage);
        }
    }
}