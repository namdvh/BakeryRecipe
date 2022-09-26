using BakeryRecipe.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Comments
{
    public class CommentPostDTO
    {
        public int CommentId { get; set; }
        
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Avatar { get; set; }

        public List<CommentPostDTO>? ReplyTo { get; set; }
    }
}
