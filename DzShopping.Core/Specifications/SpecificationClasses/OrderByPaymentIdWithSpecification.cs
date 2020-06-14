using DzShopping.Core.Models.OrderAggregate;

namespace DzShopping.Core.Specifications.SpecificationClasses
{
    public class OrderByPaymentIdWithSpecification : Specification<Order>
    {
        public OrderByPaymentIdWithSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
