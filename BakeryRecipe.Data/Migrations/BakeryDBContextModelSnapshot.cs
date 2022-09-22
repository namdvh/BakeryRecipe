﻿// <auto-generated />
using System;
using BakeryRecipe.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BakeryRecipe.Data.Migrations
{
    [DbContext(typeof(BakeryDBContext))]
    partial class BakeryDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Mon Au"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Mon A"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Nuoc trai cay"
                        });
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReplyToId")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("ReplyToId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Interactive", b =>
                {
                    b.Property<int>("InteractiveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InteractiveId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("InteractStatus")
                        .HasColumnType("int");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("InteractiveId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Interactives", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<Guid>("RetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.PostProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("PostProducts", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UnitInStock")
                        .HasColumnType("int");

                    b.Property<int>("UnitType")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.ProductCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("ProductCategories", (string)null);

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            Name = "Ingredients"
                        },
                        new
                        {
                            CategoryID = 2,
                            Name = "Tools"
                        });
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Report", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("Reports", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Repost", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("Reposts", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("52ec6e78-6732-43bf-adab-9cfa2e5da268"),
                            ConcurrencyStamp = "4abed87f-5427-49c4-b6ab-c80cfeac8038",
                            Description = "Admin",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
                            ConcurrencyStamp = "be5554dc-e52b-43e6-b3c4-405c0d72c418",
                            Description = "User",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = new Guid("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"),
                            ConcurrencyStamp = "bfdc5221-5aa8-470f-b374-ee7f08eb25e0",
                            Description = "Retailer",
                            Name = "Retailer",
                            NormalizedName = "RETAILER"
                        });
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("Provider")
                        .HasColumnType("int");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("a91d5ec0-0405-4fdc-a8bb-41cc95bdbd50"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "716ca760-f971-4516-8ccb-ccbbd49f3edf",
                            CreatedDate = new DateTime(2022, 9, 22, 2, 49, 28, 931, DateTimeKind.Utc).AddTicks(5429),
                            DOB = new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "anhkhoahuynh90@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Huynh",
                            Gender = 0,
                            LastName = "Anh Khoa",
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAEPu39u0moGKQrKgrBbx9F8mXYzDdyMOPMshag0Fxa5dxxHtWtT+pJac2RsIEwEhtiA==",
                            PhoneNumber = "0868644651",
                            PhoneNumberConfirmed = false,
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SecurityStamp = "",
                            Status = 1,
                            Token = "xxx",
                            TwoFactorEnabled = false,
                            UserName = "user@123"
                        },
                        new
                        {
                            Id = new Guid("95ac3873-ae86-4139-a4c3-97e7abc8956a"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "849e1d57-6218-44df-9e77-cd0170f0d80e",
                            CreatedDate = new DateTime(2022, 9, 22, 2, 49, 28, 938, DateTimeKind.Utc).AddTicks(8608),
                            DOB = new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "namhoaidoan15@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Doan Vu",
                            Gender = 0,
                            LastName = "Hoai Nam",
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAEEOUuwwm19HNhzuT8qcg+bZ3965w/K3RHpPDVjj6chadmxXwMdsx6byAzo5k5LoCgA==",
                            PhoneNumber = "0868644651",
                            PhoneNumberConfirmed = false,
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SecurityStamp = "",
                            Status = 1,
                            Token = "xxx",
                            TwoFactorEnabled = false,
                            UserName = "Admin@123"
                        },
                        new
                        {
                            Id = new Guid("176a6bf2-3818-4d69-b1c8-1751e182602f"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "bb8c82f8-a8b6-4786-85fe-fb9a9f546b18",
                            CreatedDate = new DateTime(2022, 9, 22, 2, 49, 28, 946, DateTimeKind.Utc).AddTicks(183),
                            DOB = new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "thinh123@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Anh",
                            Gender = 0,
                            LastName = "Thinh",
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAEPYDb1TQTQfZy9niWW5BA/d3bU5i8Z7iqvCfz4P6NMYLomWmCvLip67ODN/y15U8kA==",
                            PhoneNumber = "0868644651",
                            PhoneNumberConfirmed = false,
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SecurityStamp = "",
                            Status = 1,
                            Token = "xxx",
                            TwoFactorEnabled = false,
                            UserName = "Retailer@123"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("a91d5ec0-0405-4fdc-a8bb-41cc95bdbd50"),
                            RoleId = new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313")
                        },
                        new
                        {
                            UserId = new Guid("95ac3873-ae86-4139-a4c3-97e7abc8956a"),
                            RoleId = new Guid("52ec6e78-6732-43bf-adab-9cfa2e5da268")
                        },
                        new
                        {
                            UserId = new Guid("176a6bf2-3818-4d69-b1c8-1751e182602f"),
                            RoleId = new Guid("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Comment", b =>
                {
                    b.HasOne("BakeryRecipe.Data.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BakeryRecipe.Data.Entities.Comment", null)
                        .WithMany("ReplyTo")
                        .HasForeignKey("ReplyToId");

                    b.HasOne("BakeryRecipe.Data.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Interactive", b =>
                {
                    b.HasOne("BakeryRecipe.Data.Entities.Post", "Post")
                        .WithMany("Interactives")
                        .HasForeignKey("PostId");

                    b.HasOne("BakeryRecipe.Data.Entities.User", "User")
                        .WithMany("Interactives")
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Order", b =>
                {
                    b.HasOne("BakeryRecipe.Data.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.OrderDetail", b =>
                {
                    b.HasOne("BakeryRecipe.Data.Entities.Order", "Orders")
                        .WithMany("OrderDetail")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BakeryRecipe.Data.Entities.Product", "Products")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Post", b =>
                {
                    b.HasOne("BakeryRecipe.Data.Entities.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BakeryRecipe.Data.Entities.Category", "Category")
                        .WithMany("Post")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.PostProduct", b =>
                {
                    b.HasOne("BakeryRecipe.Data.Entities.Post", "Post")
                        .WithMany("PostProducts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BakeryRecipe.Data.Entities.Product", "Product")
                        .WithMany("PostProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Product", b =>
                {
                    b.HasOne("BakeryRecipe.Data.Entities.ProductCategory", "ProductCategorys")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BakeryRecipe.Data.Entities.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategorys");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Report", b =>
                {
                    b.HasOne("BakeryRecipe.Data.Entities.Post", "Post")
                        .WithMany("Reports")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BakeryRecipe.Data.Entities.User", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Repost", b =>
                {
                    b.HasOne("BakeryRecipe.Data.Entities.Post", "Post")
                        .WithMany("Reposts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BakeryRecipe.Data.Entities.User", "User")
                        .WithMany("Reposts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Category", b =>
                {
                    b.Navigation("Post");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Comment", b =>
                {
                    b.Navigation("ReplyTo");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Interactives");

                    b.Navigation("PostProducts");

                    b.Navigation("Reports");

                    b.Navigation("Reposts");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.Product", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("PostProducts");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("BakeryRecipe.Data.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Interactives");

                    b.Navigation("Orders");

                    b.Navigation("Posts");

                    b.Navigation("Products");

                    b.Navigation("Reports");

                    b.Navigation("Reposts");
                });
#pragma warning restore 612, 618
        }
    }
}
