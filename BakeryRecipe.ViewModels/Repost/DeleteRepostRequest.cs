using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Repost
{
    public class DeleteRepostRequest
    {
        public Guid UserId { get; set; }
        public int RepostId { get; set; }
    }
}
