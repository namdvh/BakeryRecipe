using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.Entities
{
    public class PostProduct
    {
        public int PostId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Post Post { get; set; }
        public Product Product { get; set; }
    }
}
