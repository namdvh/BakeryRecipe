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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.CommentId);
            builder.Property(x => x.CommentId).ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");


            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ReplyTo);

            builder
                .HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostId);

            builder
                .HasOne<Comment>()
                .WithOne(x=>x.ReplyTo)
                .HasForeignKey<Comment>(x => x.ReplyToId);
        }
    }
}
