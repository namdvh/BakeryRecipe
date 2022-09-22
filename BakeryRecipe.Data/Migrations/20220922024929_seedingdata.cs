using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryRecipe.Data.Migrations
{
    public partial class seedingdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Mon Au" },
                    { 2, "Mon A" },
                    { 3, "Nuoc trai cay" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { 1, "Ingredients" },
                    { 2, "Tools" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("52ec6e78-6732-43bf-adab-9cfa2e5da268"), "4abed87f-5427-49c4-b6ab-c80cfeac8038", "Admin", "Admin", "ADMIN" },
                    { new Guid("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"), "bfdc5221-5aa8-470f-b374-ee7f08eb25e0", "Retailer", "Retailer", "RETAILER" },
                    { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), "be5554dc-e52b-43e6-b3c4-405c0d72c418", "User", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"), new Guid("176a6bf2-3818-4d69-b1c8-1751e182602f") },
                    { new Guid("52ec6e78-6732-43bf-adab-9cfa2e5da268"), new Guid("95ac3873-ae86-4139-a4c3-97e7abc8956a") },
                    { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), new Guid("a91d5ec0-0405-4fdc-a8bb-41cc95bdbd50") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Avatar", "Code", "ConcurrencyStamp", "CreatedDate", "DOB", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Provider", "RefreshTokenExpiryTime", "SecurityStamp", "Status", "Token", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("176a6bf2-3818-4d69-b1c8-1751e182602f"), 0, null, null, null, "bb8c82f8-a8b6-4786-85fe-fb9a9f546b18", new DateTime(2022, 9, 22, 2, 49, 28, 946, DateTimeKind.Utc).AddTicks(183), new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "thinh123@gmail.com", false, "Anh", 0, "Thinh", false, null, null, null, "AQAAAAEAACcQAAAAEPYDb1TQTQfZy9niWW5BA/d3bU5i8Z7iqvCfz4P6NMYLomWmCvLip67ODN/y15U8kA==", "0868644651", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1, "xxx", false, "Retailer@123" },
                    { new Guid("95ac3873-ae86-4139-a4c3-97e7abc8956a"), 0, null, null, null, "849e1d57-6218-44df-9e77-cd0170f0d80e", new DateTime(2022, 9, 22, 2, 49, 28, 938, DateTimeKind.Utc).AddTicks(8608), new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "namhoaidoan15@gmail.com", false, "Doan Vu", 0, "Hoai Nam", false, null, null, null, "AQAAAAEAACcQAAAAEEOUuwwm19HNhzuT8qcg+bZ3965w/K3RHpPDVjj6chadmxXwMdsx6byAzo5k5LoCgA==", "0868644651", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1, "xxx", false, "Admin@123" },
                    { new Guid("a91d5ec0-0405-4fdc-a8bb-41cc95bdbd50"), 0, null, null, null, "716ca760-f971-4516-8ccb-ccbbd49f3edf", new DateTime(2022, 9, 22, 2, 49, 28, 931, DateTimeKind.Utc).AddTicks(5429), new DateTime(2021, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "anhkhoahuynh90@gmail.com", false, "Huynh", 0, "Anh Khoa", false, null, null, null, "AQAAAAEAACcQAAAAEPu39u0moGKQrKgrBbx9F8mXYzDdyMOPMshag0Fxa5dxxHtWtT+pJac2RsIEwEhtiA==", "0868644651", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1, "xxx", false, "user@123" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("52ec6e78-6732-43bf-adab-9cfa2e5da268"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"), new Guid("176a6bf2-3818-4d69-b1c8-1751e182602f") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("52ec6e78-6732-43bf-adab-9cfa2e5da268"), new Guid("95ac3873-ae86-4139-a4c3-97e7abc8956a") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), new Guid("a91d5ec0-0405-4fdc-a8bb-41cc95bdbd50") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("176a6bf2-3818-4d69-b1c8-1751e182602f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("95ac3873-ae86-4139-a4c3-97e7abc8956a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a91d5ec0-0405-4fdc-a8bb-41cc95bdbd50"));
        }
    }
}
