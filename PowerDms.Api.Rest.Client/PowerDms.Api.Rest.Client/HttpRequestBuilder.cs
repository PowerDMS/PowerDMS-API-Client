using System.Net.Http;

namespace PowerDms.Api.Rest.Client
{
    using PowerDms.Api.Rest.Dto;

    public class HttpRequestBuilder<TResponse>
    {
        public HttpRequestMessage HttpRequestMessage;

        public Credentials Credentials;

        public HttpRequestBuilder(
            HttpRequestMessage httpRequestMessage)
        {
            HttpRequestMessage = httpRequestMessage;
        }

        public HttpRequestBuilder<TResponse> AuthenticateWith(
            Credentials credentials)
        {
            Credentials = credentials;
            return this;
        }
    }
}