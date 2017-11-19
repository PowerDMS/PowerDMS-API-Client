using System.Threading.Tasks;
using PowerDms.Api.Rest.Client.Clients;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        private readonly OAuthClient _OAuthClient;

        public AuthenticationTokenProvider(OAuthClient oAuthClient)
        {
            _OAuthClient = oAuthClient;
        }

        public async Task<string> GetAccessToken(Credentials credentials)
        {
            return await (await _OAuthClient.GetAccessToken(
                credentials.Username,
                credentials.Password,
                credentials.SiteKey,
                credentials.ClientSecret
            )).GetContent<string>();
        }
    }
}