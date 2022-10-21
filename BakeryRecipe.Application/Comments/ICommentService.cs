using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Statictis;
using BakeryRecipe.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Users
{
    public interface ICommentService
    {
        Task<BaseResponse<string>> AddComment(CommentRequestDTO request);

        Task<BaseResponse<List<StaticstisMonth>>> GetStaticCommentsMonth();
        Task<BaseResponse<List<StaticstisYear>>> GetStaticCommentsYear();
        Task<BaseResponse<List<StaticstisDay>>> GetStaticCommentsDay();
    }
}
