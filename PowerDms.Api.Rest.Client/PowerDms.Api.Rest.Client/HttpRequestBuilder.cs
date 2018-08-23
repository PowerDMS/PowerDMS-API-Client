using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestBuilder<TResponse>
    {
        // TODO: replace this when .net std 2.1 is out:
        // https://github.com/dotnet/corefx/issues/26201
        private static string _applicationJson = "application/json";

        public HttpRequestMessage HttpRequestMessage;
        
        public object Body { get; private set; }

        public HttpRequestBuilder(
            HttpRequestMessage httpRequestMessage)
        {
            HttpRequestMessage = httpRequestMessage;
        }

        public HttpRequestBuilder<TResponse> AddJsonBody(object body)
        {
            // NOTE: I defined this here instead of using an extension method, because FakeItEasy can't fake extension methods
            Body = body;
            var json = JsonConvert.SerializeObject(body);
            HttpRequestMessage.Content = new StringContent(json, Encoding.UTF8, _applicationJson);
            return this;
        }
    }
}