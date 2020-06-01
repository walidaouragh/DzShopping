using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DzShopping.API.Dtos
{
    public class CustomerCartDto
    {
        [Required]
        public string CartId { get; set; }

        public List<CartItemDto> CartItems { get; set; }

        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }

        public decimal shippingPrice { get; set; }
    }
}
