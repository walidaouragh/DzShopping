using Microsoft.AspNetCore.Identity;

namespace DzShopping.Core.Models.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
