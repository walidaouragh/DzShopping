using System.Threading.Tasks;
using AutoMapper;
using DzShopping.API.Dtos;
using DzShopping.Core.Models;
using DzShopping.Infrastructure.Repositories.CartRepository;
using Microsoft.AspNetCore.Mvc;

namespace DzShopping.API.Controllers.CartController
{
    public class CartController : BaseApiController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetCartById(string cartId)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);
            return Ok(cart ?? new CustomerCart(cartId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerCart>> UpdateCart(CustomerCartDto cart)
        {
            var customerCart = _mapper.Map<CustomerCartDto, CustomerCart>(cart);
            var updatedCart = await _cartRepository.UpdateCartAsync(customerCart);
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task<ActionResult<CustomerCart>> DeleteCart(string cartId)
        {
            return Ok(await _cartRepository.DeleteCartAsync(cartId));
        }
    }
}