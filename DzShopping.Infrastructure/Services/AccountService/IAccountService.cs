using DzShopping.Core.Models.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.Services.AccountService
{
    public interface IAccountService
    {
        Task<AppUser> GetCurrentUser(ClaimsPrincipal user);
        Task<AppUser> GetUserAddress(ClaimsPrincipal user);
    }
}
