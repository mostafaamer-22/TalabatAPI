using Talabat.DAL.Entities;

namespace Talabat.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }

        public int? DeliveryMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }

 
}
