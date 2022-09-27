using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Users
{
    public class CommentRequestDTO
    {
        public Guid UserId { get; set; }
        public int PostId { get; set; } 
        public string Content { get; set; }
        public int? ReplyId { get; set; }
        public string? Code { get; set; }
    }
}
