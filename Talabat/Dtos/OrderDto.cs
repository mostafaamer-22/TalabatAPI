namespace Talabat.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public UserAddressDto ShipToAddress { get; set; }
    }
}
