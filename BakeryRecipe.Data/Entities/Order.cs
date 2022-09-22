using BakeryRecipe.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.Entities
{
    public class Order
    {
        public int OrderId {get;set;}
        public Guid? UserId { get;set;}
        public decimal Total { get;set;}
        public Status Status { get; set; }
        public Guid RetailId { get; set; }
        public DateTime CreatedDate { get; set; }
        public User? User { get; set; } 
        public List<OrderDetail> OrderDetail { get; set; } 
    }
}
