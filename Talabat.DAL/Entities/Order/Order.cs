using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.DAL.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail,  Address shipToAddress, DeliveryMethod deliveryMethod, 
            IReadOnlyList<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public Address ShipToAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public IReadOnlyList<OrderItem> Items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GetTotal() 
            => SubTotal + DeliveryMethod.Cost;
    }
}
