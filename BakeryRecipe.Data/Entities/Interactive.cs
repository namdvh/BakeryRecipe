using BakeryRecipe.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.Entities
{
    public class Interactive
    {
        public int InteractiveId { get; set; }
        public DateTime CreatedDate { get; set; }
        public InteractiveStatus InteractStatus { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
       
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
