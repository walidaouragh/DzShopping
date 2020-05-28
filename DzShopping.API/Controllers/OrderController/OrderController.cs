using AutoMapper;
using DzShopping.API.Dtos;
using DzShopping.API.Errors;
using DzShopping.API.Extensions;
using DzShopping.Core.Models.OrderAggregate;
using DzShopping.Infrastructure.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DzShopping.API.Controllers.OrderController
{
    public class OrderController: BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = HttpContext.User.RetreiveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippToAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.DeliveryMethodId, orderDto.CartId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersByUserEmail()
        {
            var buyerEmail = HttpContext.User.RetreiveEmailFromPrincipal();
            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderByOrderIdAndUserEmail(int orderId)
        {
            var buyerEmail = HttpContext.User.RetreiveEmailFromPrincipal();
            var order = await _orderService.GetOrderByIdAsync(orderId, buyerEmail);

            if (order == null) return BadRequest(new ApiResponse(404));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }


        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {

            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
