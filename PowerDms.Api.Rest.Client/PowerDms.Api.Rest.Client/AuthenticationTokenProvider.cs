using System.Collections.Generic;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Client.Clients;

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

            return await (await _OAuthClient.GetAccessToken(
                credentials.Username,
                credentials.Password,
                credentials.SiteKey,
                credentials.ClientSecret
            )).GetContent<string>();
        }
    }
}