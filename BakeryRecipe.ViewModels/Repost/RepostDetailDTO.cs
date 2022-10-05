using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Repost
{
    public class RepostDetailDTO
    {
        public Guid UserId { get; set; }
        public List<PostDTO> Post { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
