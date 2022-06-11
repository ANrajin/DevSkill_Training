
using System.Security.Claims;

namespace FirstDemo.Membership.Services
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
}