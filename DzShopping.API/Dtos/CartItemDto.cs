using System;
using System.ComponentModel.DataAnnotations;

namespace DzShopping.API.Dtos
{
    public class CartItemDto
    {
        [Required]
        public int CartItemId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be greater than 1")]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string ProductBrand { get; set; }

        [Required]
        public string ProductType { get; set; }
    }
}
