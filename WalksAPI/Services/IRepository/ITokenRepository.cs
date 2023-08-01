using Microsoft.AspNetCore.Identity;

namespace WalksAPI.Services.IRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
