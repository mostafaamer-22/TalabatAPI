using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.DAL.Entities.Order;

namespace Talabat.DAL.Data.config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShipToAddress, NP => {

                NP.WithOwner();
              
            });

            builder.Property(O => O.Status).HasConversion(
                   OrderStatus => OrderStatus.ToString(),
                   OrderStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OrderStatus)
                ); ;
            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
