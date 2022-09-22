using BakeryRecipe.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductImage { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }  
        public DateTime CreatedDate { get; set; }
        public int UnitInStock { get; set; }
        public Enum.Type UnitType { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public List<PostProduct> PostProducts { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategorys { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } 

    }
}
