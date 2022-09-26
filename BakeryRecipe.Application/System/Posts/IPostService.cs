using BakeryRecipe.Application.ClaimTokens;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Posts;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Posts
{
    public interface IPostService
    {
        Task<bool> CreatePost(AddPostRequest request);

        Task<bool> UpdatePost(UpdatePostRequest request,int postID);

        Task<BasePagination<List<PostDTO>>> GetPost(PaginationFilter filter);

        Task<BaseResponse<PostDetailDTO>> GetDetailPost(int id);

    }
}
