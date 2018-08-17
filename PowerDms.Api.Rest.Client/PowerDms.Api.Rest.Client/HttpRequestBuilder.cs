using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestBuilder<TResponse>
    {
        public HttpRequestMessage HttpRequestMessage;

        public Credentials Credentials;
        
        public object Body { get; private set; }

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

        public HttpRequestBuilder<TResponse> AddJsonBody(object body)
        {
            Body = body;
            var json = JsonConvert.SerializeObject(body);
            HttpRequestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return this;
        }
    }
}