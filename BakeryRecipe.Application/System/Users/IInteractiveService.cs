using BakeryRecipe.ViewModels.Interactive;
using BakeryRecipe.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Users
{
    public interface IInteractiveService
    {
        Task<BaseResponse<string>> LikeOrDislike(Guid UserId, int PostId,bool IsLike);

        Task<BaseResponse<InteractiveStatusRequest>> GetInteractiveStatus(Guid userID, int PostId);
    }
}
