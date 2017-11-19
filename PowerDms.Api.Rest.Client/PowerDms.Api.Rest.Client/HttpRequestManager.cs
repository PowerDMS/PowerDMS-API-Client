using System;
using System.Net.Http;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestManager
    {
        private readonly HttpClient _HttpClient;

        public HttpRequestManager(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public HttpRequestBuilder CreateHttpRequestBuilder(HttpRequestMessage httpRequestMessage)
        {
            return new HttpRequestBuilder(httpRequestMessage, _HttpClient);
        }
    }
}