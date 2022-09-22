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
    public class InteractiveConfiguration : IEntityTypeConfiguration<Interactive>
    {
        public void Configure(EntityTypeBuilder<Interactive> builder)
        {
            builder.ToTable("Interactives");
            builder.HasKey(x => x.InteractiveId);
            builder.Property(x => x.InteractiveId).ValueGeneratedOnAdd();
            builder.Property(x => x.InteractStatus).IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
            builder
                .HasOne<User>(x => x.User)
                .WithMany(x => x.Interactives)
                .HasForeignKey(x => x.UserId);
            builder
                .HasOne<Post>(x => x.Post)
                .WithMany(x => x.Interactives)
                .HasForeignKey(x => x.PostId);
        }
    }
}
