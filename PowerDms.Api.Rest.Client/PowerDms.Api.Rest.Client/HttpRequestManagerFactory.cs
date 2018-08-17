using System.Net.Http;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestManagerFactory
    {
        /// <summary>
        /// non static to help facilitate dependency injection during unit testing
        /// </summary>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public HttpRequestManager CreateInstance(HttpClient httpClient)
        {
            return new HttpRequestManager(httpClient);
        }
    }
}