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

        Task<bool> DeletePost(int postID);

        Task<BasePagination<List<PostDTO>>> GetPost(PaginationFilter filter);

        Task<BasePagination<List<PostDTO>>> SearchPostByCategories(PaginationFilter filter,int categoriesID);

        Task<BasePagination<List<PostDTO>>> SearchPostByName(PaginationFilter filter,string keyword);

        Task<BaseResponse<PostDetailDTO>> GetDetailPost(int id);

    }
}
