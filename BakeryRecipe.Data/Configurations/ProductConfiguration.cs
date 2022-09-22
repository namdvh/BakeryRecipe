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
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.ProductId).ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.ProductCategorys)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId);
        }
    }
}
