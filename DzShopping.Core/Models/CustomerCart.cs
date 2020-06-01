using System.Collections.Generic;

namespace DzShopping.Core.Models
{
    public class CustomerCart
    {
        public CustomerCart()
        {
        }

        public CustomerCart(string cartId)
        {
            CartId = cartId;
        }

        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal shippingPrice { get; set; }
    }
}
