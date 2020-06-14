using DzShopping.Core.Models;
using DzShopping.Core.Models.OrderAggregate;
using DzShopping.Core.Specifications.SpecificationClasses;
using DzShopping.Infrastructure.Repositories.CartRepository;
using DzShopping.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DzShopping.Infrastructure.Services.PaymentService;

namespace DzShopping.Infrastructure.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepo;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, ICartRepository cartRepo, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _cartRepo = cartRepo;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string cartId, Address shippingAddress)
        {
            // get cart from repo
            var cart = await _cartRepo.GetCartAsync(cartId);

            // get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in cart.CartItems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.CartItemId);
                var itemOrdered = new ProductItemOrdered(productItem.ProductId, productItem.ProductName, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            // get delivery method from repo
            var deliveryMethode = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // calculate subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // check to see if order exists
            var specification = new OrderByPaymentIdWithSpecification(cart.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetWithSpecification(specification);

            if (existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(cart.PaymentIntentId);
            }

            // create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethode, subtotal, cart.PaymentIntentId);
            _unitOfWork.Repository<Order>().Add(order);

            // save to db
            var result = await _unitOfWork.Complete();
            // if nothing saved to the db
            if (result <= 0) return null;

            // return order
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var specification = new OrderWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().GetListWithSpecification(specification);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            var specification = new OrderWithItemsAndOrderingSpecification(orderId, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetWithSpecification(specification);
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetListAllAsync();
        }
    }
}
