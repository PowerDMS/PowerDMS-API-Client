using System.Collections.Generic;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Client.Clients;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        private readonly OAuthClient _OAuthClient;

        private IDictionary<Credentials, string> _CachedCredentials;

        public AuthenticationTokenProvider(OAuthClient oAuthClient)
        {
            _OAuthClient = oAuthClient;
            _CachedCredentials = new Dictionary<Credentials, string>();
        }

        public async Task<string> GetAccessToken(Credentials credentials)
        {
            if (_CachedCredentials.ContainsKey(credentials))
            {
                return _CachedCredentials[credentials];
            }

            var response = await _OAuthClient.GetAccessToken(
                credentials.Username,
                credentials.Password,
                credentials.SiteKey,
                credentials.ClientSecret
            );

            var authorization = await response.GetContent<OAuthAuthorizationDto>();

            return authorization.access_token;
        }
    }
}