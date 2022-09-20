using BakeryRecipe.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => x.OrderDetailId);
            builder.Property(x => x.OrderDetailId).ValueGeneratedOnAdd();


            builder
                .HasOne(x => x.Products)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.ProductId);

            
        }
    }
}
