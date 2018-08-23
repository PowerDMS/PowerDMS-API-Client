using System.Net.Http.Headers;
using System.Threading.Tasks;
using PowerDms.Api.Rest.Client.Clients;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        // TODO: must implement token refreshing at some point

        private readonly OAuthClient _oAuthClient;

        private readonly Credentials _credentials;

        private OAuthAuthorizationDto _authAuthorization;

        public AuthenticationTokenProvider(OAuthClient oAuthClient, Credentials credentials)
        {
            _oAuthClient = oAuthClient;
            _credentials = credentials;
        }

        public async Task<AuthenticationHeaderValue> GetAccessToken()
        {
            if (_authAuthorization?.access_token == null)
            {
                var response = await _oAuthClient.GetAccessToken(
                    _credentials.Username,
                    _credentials.Password,
                    _credentials.SiteKey,
                    _credentials.ClientSecret
                );

                _authAuthorization = await response.GetContent<OAuthAuthorizationDto>();
            }

            return new AuthenticationHeaderValue("Bearer", _authAuthorization.access_token);
        }
    }
}