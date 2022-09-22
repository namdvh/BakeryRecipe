using BakeryRecipe.Data.Entities;
using BakeryRecipe.Data.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = 1,
                    CategoryName = "Mon Au"
                },
                new Category()
                {
                    CategoryId = 2,
                    CategoryName = "Mon A"
                },
                new Category()
                {
                    CategoryId = 3,
                    CategoryName = "Nuoc trai cay"
                });
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory()
                {
                    CategoryID = 1,
                    Name = "Ingredients",
                },
                new ProductCategory()
                {
                    CategoryID = 2,
                    Name = "Tools",
                }
                );
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("52ec6e78-6732-43bf-adab-9cfa2e5da268"),
                Description = "Admin",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
                new Role
                {
                    Id = Guid.Parse("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
                    Description = "User",
                    Name = "User",
                    NormalizedName = "USER"
                },
               new Role
               {
                   Id = Guid.Parse("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"),
                   Description = "Retailer",
                   Name = "Retailer",
                   NormalizedName = "RETAILER"
               }
                );
                modelBuilder.Entity<User>().HasData(new User
                {
                    Id = Guid.Parse("a91d5ec0-0405-4fdc-a8bb-41cc95bdbd50"),
                    UserName="user@123",
                    Email = "anhkhoahuynh90@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "1"),
                    SecurityStamp = string.Empty,
                    FirstName = "Huynh",
                    LastName = "Anh Khoa",
                    DOB = new DateTime(2021, 07, 12),
                    PhoneNumber = "0868644651",
                    Gender = Gender.MALE,
                    Token = "xxx",
                    Status = Status.ACTIVE,
                    CreatedDate = DateTime.UtcNow
                });
                modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
                {
                    RoleId = Guid.Parse("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
                    UserId = Guid.Parse("a91d5ec0-0405-4fdc-a8bb-41cc95bdbd50"),
                });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("95ac3873-ae86-4139-a4c3-97e7abc8956a"),
                UserName = "Admin@123",
                Email = "namhoaidoan15@gmail.com",
                PasswordHash = hasher.HashPassword(null, "1"),
                SecurityStamp = string.Empty,
                FirstName = "Doan Vu",
                LastName = "Hoai Nam",
                DOB = new DateTime(2021, 07, 12),
                PhoneNumber = "0868644651",
                Gender = Gender.MALE,
                Token = "xxx",
                Status = Status.ACTIVE,
                CreatedDate = DateTime.UtcNow
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("52ec6e78-6732-43bf-adab-9cfa2e5da268"),
                UserId = Guid.Parse("95ac3873-ae86-4139-a4c3-97e7abc8956a"),
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("176a6bf2-3818-4d69-b1c8-1751e182602f"),
                UserName = "Retailer@123",
                Email = "thinh123@gmail.com",
                PasswordHash = hasher.HashPassword(null, "1"),
                SecurityStamp = string.Empty,
                FirstName = "Anh",
                LastName = "Thinh",
                DOB = new DateTime(2021, 07, 12),
                PhoneNumber = "0868644651",
                Gender = Gender.MALE,
                Token = "xxx",
                Status = Status.ACTIVE,
                CreatedDate = DateTime.UtcNow
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"),
                UserId = Guid.Parse("176a6bf2-3818-4d69-b1c8-1751e182602f"),
            });
        }
    }
}
