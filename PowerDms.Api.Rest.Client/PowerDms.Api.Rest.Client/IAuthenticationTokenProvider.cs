using System.Threading.Tasks;

namespace PowerDms.Api.Rest.Client
{
    public interface IAuthenticationTokenProvider
    {
        Task<string> GetAccessToken(Credentials credentials);
    }
}