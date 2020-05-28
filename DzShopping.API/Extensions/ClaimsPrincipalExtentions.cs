using System.Linq;
using System.Security.Claims;

namespace DzShopping.API.Extensions
{
    public static class ClaimsPrincipalExtentions
    {
        public static string RetreiveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}
