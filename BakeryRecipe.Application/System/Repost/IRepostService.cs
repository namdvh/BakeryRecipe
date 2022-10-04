using BakeryRecipe.ViewModels.Repost;
using BakeryRecipe.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Repost
{
    public interface IRepostService
    {
        public Task<BaseResponse<string>> CreateRepost(CreateRepostRequest request);
        public Task<BaseResponse<List<RepostDetailDTO>>> GetRepostByUsID(Guid UserID);
        public Task<bool> DeleteRepost(DeleteRepostRequest request);
    }
}
