using System.Collections.Generic;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Client.Clients;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        private readonly OAuthClient _OAuthClient;

        private readonly string _OAuthClientSecret;

        private IDictionary<Credentials, string> _CachedCredentials;

        public AuthenticationTokenProvider(
            OAuthClient oAuthClient,
            string oAuthClientSecret)
        {
            _OAuthClient = oAuthClient;
            _OAuthClientSecret = oAuthClientSecret;
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
                _OAuthClientSecret
            )).GetContent<string>();
        }
    }
}