using DzShopping.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.Services.AccountService
{
    // TO DO: add all AccountController logic here!
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AppUser> GetCurrentUser(ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await _userManager.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<AppUser> GetUserAddress(ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await _userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
