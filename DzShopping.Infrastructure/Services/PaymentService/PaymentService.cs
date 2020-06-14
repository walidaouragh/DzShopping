using DzShopping.Core.Models;
using DzShopping.Core.Models.OrderAggregate;
using DzShopping.Infrastructure.Repositories.CartRepository;
using DzShopping.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DzShopping.Core.Specifications.SpecificationClasses;
using Order = DzShopping.Core.Models.OrderAggregate.Order;
using Product = DzShopping.Core.Models.Product;

namespace DzShopping.Infrastructure.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(ICartRepository cartRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<CustomerCart> CreateOrUpdatePaymentIntent(string cartId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var cart = await _cartRepository.GetCartAsync(cartId);

            if (cart == null)
            {
                return null;
            }

            // m for money
            var shippingPrice = 0m;

            if (cart.DeliveryMethodId.HasValue)
            {
                var deliveryMethode = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)cart.DeliveryMethodId);
                shippingPrice = deliveryMethode.Price;
            }

            foreach (var item in cart.CartItems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.CartItemId);

                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            // PaymentIntentService coming from stripe
            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)cart.CartItems.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)cart.CartItems.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
                };

                await service.UpdateAsync(cart.PaymentIntentId, options);
            }

            await _cartRepository.UpdateCartAsync(cart);

            return cart;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var specification = new OrderByPaymentIdWithSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecification(specification);

            if (order == null) return null;

            order.Status = OrderStatus.PaymentRecevied;
            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Complete();

            return order;
        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var specification = new OrderByPaymentIdWithSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecification(specification);

            if (order == null) return null;

            order.Status = OrderStatus.PaymentFailed;
            await _unitOfWork.Complete();

            return order;
        }
    }
}
