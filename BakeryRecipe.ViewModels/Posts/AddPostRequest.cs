using BakeryRecipe.ViewModels.PostProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Posts
{
    public class AddPostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public Guid AuthorID { get; set; }
        public int CategoryID { get; set; }

        public List<AddPostProductDTO> PostProduct { get; set; }
    }
}
