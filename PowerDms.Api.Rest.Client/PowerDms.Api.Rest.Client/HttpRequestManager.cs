using System;
using System.Net.Http;
using PowerDms.Api.Rest.Client.Clients;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestManager
    {
        private readonly HttpClient _HttpClient;

        public readonly PowerDmsRestApiClient PowerDmsRestApiClient;

        private readonly IAuthenticationTokenProvider _AuthenticationTokenProvider;

        public HttpRequestManager(
            HttpClient httpClient)
        {
            _HttpClient = httpClient;
            PowerDmsRestApiClient = new PowerDmsRestApiClient(httpClient);
            _AuthenticationTokenProvider = new AuthenticationTokenProvider(
                PowerDmsRestApiClient.OAuth);
        }

        public HttpRequestBuilder CreateHttpRequestBuilder(HttpRequestMessage httpRequestMessage)
        {
            return new HttpRequestBuilder(httpRequestMessage, _HttpClient, _AuthenticationTokenProvider);
        }
    }
}