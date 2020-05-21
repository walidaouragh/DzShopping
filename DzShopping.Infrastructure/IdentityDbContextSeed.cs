using DzShopping.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure
{
    public class IdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Walid",
                    Email = "walid@test.com",
                    UserName = "walid@test.com",
                    Address = new Address
                    {
                        FirstName = "Walid",
                        LastName = "Walid lastname",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
