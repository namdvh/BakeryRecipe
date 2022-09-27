using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Interactive
{
    public class LikeActionRequest
    {
        public Guid UserID { get; set; }
        public int PostID { get; set; }
        public bool IsLike { get; set; }
        public string? Code { get; set; }
    }
}
