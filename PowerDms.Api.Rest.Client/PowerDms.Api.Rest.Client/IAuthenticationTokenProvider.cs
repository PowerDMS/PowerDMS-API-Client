using System.Threading.Tasks;
using PowerDms.Api.Rest.Dto;

namespace PowerDms.Api.Rest.Client
{
    public interface IAuthenticationTokenProvider
    {
        Task<string> GetAccessToken(Credentials credentials);
    }
}