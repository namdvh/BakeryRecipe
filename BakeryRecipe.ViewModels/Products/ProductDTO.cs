using BakeryRecipe.Data.Entities;
using BakeryRecipe.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Products
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Data.Enum.Type Type { get; set; }  
        public string? ProductImage { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UnitInStock { get; set; }
    }
}
