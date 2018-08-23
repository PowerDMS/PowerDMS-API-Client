using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PowerDms.Api.Rest.Client
{
    public interface IAuthenticationTokenProvider
    {
        Task<AuthenticationHeaderValue> GetAccessToken();
    }
}