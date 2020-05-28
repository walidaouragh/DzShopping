namespace DzShopping.API.Dtos
{
    public class OrderDto
    {
        public int DeliveryMethodId { get; set; }
        public string CartId { get; set; }
        public AddressDto ShippToAddress { get; set; }
    }
}
