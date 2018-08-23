using PowerDms.Api.Rest.Client.Clients;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public class HttpRequestManagerBuilder
    {
        private string _apiHost;

        private string _oAuthHost;

        private ApiVersion _version;

        private IAuthenticationTokenProvider _authenticationTokenProvider;

        public HttpRequestManagerBuilder()
        {
            _apiHost = RestApiRoutes.PowerDmsRestApiDomain;
            _oAuthHost = RestApiRoutes.PowerDmsOAuthDomain;
            _version = ApiVersion.Version1;
        }

        /// <summary>
        /// Override the PowerDMS REST API host, normally not needed
        /// </summary>
        /// <param name="hostDomain"></param>
        /// <returns></returns>
        public virtual HttpRequestManagerBuilder SetApiHost(string hostDomain)
        {
            _apiHost = hostDomain;
            return this;
        }

        /// <summary>
        /// Override the PowerDMS REST API OAuth server address, normally not needed
        /// </summary>
        /// <param name="hostDomain"></param>
        /// <returns></returns>
        public virtual HttpRequestManagerBuilder SetOAuthHost(string hostDomain)
        {
            _oAuthHost = hostDomain;
            return this;
        }

        public virtual HttpRequestManagerBuilder SetApiVersion(ApiVersion version)
        {
            _version = version;
            return this;
        }

        public virtual HttpRequestManagerBuilder SetAuthenticationTokenProvider(IAuthenticationTokenProvider authenticationTokenProvider)
        {
            _authenticationTokenProvider = authenticationTokenProvider;
            return this;
        }

        public virtual HttpRequestManager Build(Credentials credentials)
        {
            var apiHttpClient = PowerDmsHttpClientFactory.CreateHttpClient(_apiHost, _version);
            var oAuthHttpClient = PowerDmsHttpClientFactory.CreateHttpClient(_oAuthHost, null);

            var authProvider = _authenticationTokenProvider ??
                               new AuthenticationTokenProvider(
                                   new OAuthClient(oAuthHttpClient), credentials
                               );

            return new HttpRequestManager(apiHttpClient, authProvider);
        }
    }
}