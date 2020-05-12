using System.Threading.Tasks;
using DzShopping.Core.Models;
using DzShopping.Infrastructure.Repositories.CartRepository;
using Microsoft.AspNetCore.Mvc;

namespace DzShopping.API.Controllers.CartController
{
    public class CartController : BaseApiController
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetCartById(string cartId)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);
            return Ok(cart ?? new CustomerCart(cartId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerCart>> UpdateCart(CustomerCart cart)
        {
            var updatedCart = await _cartRepository.UpdateCartAsync(cart);
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task<ActionResult<CustomerCart>> DeleteCart(string cartId)
        {
            return Ok(await _cartRepository.DeleteCartAsync(cartId));
        }
    }
}