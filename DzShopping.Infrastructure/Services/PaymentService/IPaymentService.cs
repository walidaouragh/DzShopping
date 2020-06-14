using DzShopping.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DzShopping.Core.Models.OrderAggregate;

namespace DzShopping.Infrastructure.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerCart> CreateOrUpdatePaymentIntent(string cartId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);

        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
