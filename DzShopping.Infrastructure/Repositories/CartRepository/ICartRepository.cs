using System.Threading.Tasks;
using DzShopping.Core.Models;

namespace DzShopping.Infrastructure.Repositories.CartRepository
{
    public interface ICartRepository
    {
        Task<CustomerCart> GetCartAsync(string cartId);
        Task<CustomerCart> UpdateCartAsync(CustomerCart cart);
        Task<bool> DeleteCartAsync(string cartId);
    }
}
