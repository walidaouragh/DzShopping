using DzShopping.Core.Models.Identity;

namespace DzShopping.Infrastructure.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(AppUser user);
    };
}
