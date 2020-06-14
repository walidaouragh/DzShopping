using DzShopping.Core.Models;
using DzShopping.Infrastructure.Services.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using DzShopping.API.Errors;
using Microsoft.Extensions.Logging;
using Stripe;
using Order = DzShopping.Core.Models.OrderAggregate.Order;

namespace DzShopping.API.Controllers.PaymentsController
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<IPaymentService> _logger;
        private const string WebhooksSecret = "whsec_obdmDyuJawu0a60rBiEDKMN4dzqKtfRV";

        public PaymentsController(IPaymentService paymentService, ILogger<IPaymentService> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<CustomerCart>> CreateOrUpdatePaymentIntent(string cartId)
        {
            var cart = await _paymentService.CreateOrUpdatePaymentIntent(cartId);

            if (cart == null)
            {
                return BadRequest(new ApiResponse(400, "Problem with your cart"));
            }
            return cart;

        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WebhooksSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded: ", intent.Id);
                    order  = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received: ", order.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed: ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Payment Failed: ", order.Id);
                    break;
            }

            return new EmptyResult();
        }

    }
}
