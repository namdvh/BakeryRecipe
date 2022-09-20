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
    public class PostProductConfigurations : IEntityTypeConfiguration<PostProduct>
    {
        public void Configure(EntityTypeBuilder<PostProduct> builder)
        {
            builder.ToTable("PostProducts");
            builder.HasKey(x => new { x.ProductId, x.PostId });

            builder
                   .HasOne(x => x.Post)
                   .WithMany(x => x.PostProducts)
                   .HasForeignKey(x => x.PostId);
            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.PostProducts)
                .HasForeignKey(x => x.ProductId);

        }
    }
