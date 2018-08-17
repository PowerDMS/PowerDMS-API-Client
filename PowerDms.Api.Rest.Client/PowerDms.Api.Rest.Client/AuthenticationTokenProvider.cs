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

        public async Task<HttpAuthorization> GetAccessToken(Credentials credentials)
        {
            if (_CachedCredentials.ContainsKey(credentials))
            {
                return new HttpAuthorization
                {
                    Type = "Bearer",
                    Credentials = _CachedCredentials[credentials]
                };
            }

            var response = await _OAuthClient.GetAccessToken(
                credentials.Username,
                credentials.Password,
                credentials.SiteKey,
                credentials.ClientSecret
            );

            var authorization = await response.GetContent<OAuthAuthorizationDto>();

            // CR note: missing storing to cache
            // but I don't know, I'll rather token be stored in the Manager

            return new HttpAuthorization
            {
                Type = "Bearer",
                Credentials = authorization.access_token
            };
        }
    }
}