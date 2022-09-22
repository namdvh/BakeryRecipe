using BakeryRecipe.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } 
        public Status Status { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Like { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public List<Interactive> Interactives { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Report>? Reports { get; set; }
        public List<Repost>? Reposts { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<PostProduct> PostProducts { get; set; }
    }
}
