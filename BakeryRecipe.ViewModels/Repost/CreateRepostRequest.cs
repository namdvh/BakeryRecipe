using BakeryRecipe.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Repost
{
    public class CreateRepostRequest
    {
        public Guid UserId { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
