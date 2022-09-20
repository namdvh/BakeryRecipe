using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.Entities
{
    public class ProductCategory
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public Product Products { get; set; }
    }
}
