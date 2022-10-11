using BakeryRecipe.ViewModels.Comments;
using BakeryRecipe.ViewModels.PostProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Posts
{
    public class PostDetailDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public int Like { get; set; }
        public Guid AuthorID { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorAvatar { get; set; }
        public int? CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<PostProductDTO>? PostProducts { get; set; }

        public List<CommentPostDTO>? Comments { get; set; }
    }
}
