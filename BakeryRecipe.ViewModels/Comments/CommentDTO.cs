using BakeryRecipe.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Comments
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        public List<BakeryRecipe.Data.Entities.Comment>? ReplyTo { get; set; }
        public int? ReplyToId { get; set; }
    }
}
