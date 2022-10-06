using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Interactive;
using BakeryRecipe.ViewModels.Response;
using Microsoft.EntityFrameworkCore;
namespace BakeryRecipe.Application.System.Users
{
    public class InteractiveService : IInteractiveService
    {
      
        private readonly BakeryDBContext _context;
        public InteractiveService(BakeryDBContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<InteractiveStatusRequest>> GetInteractiveStatus(Guid userID, int PostId)
        {
            BaseResponse<InteractiveStatusRequest> response = new();
            InteractiveStatusRequest status = new();

            var rs = await _context.Interactives.FirstOrDefaultAsync(x => x.UserId.Equals(userID) && x.PostId.Equals(PostId));
            var rsSave = await _context.Reposts.FirstOrDefaultAsync(x => x.UserId.Equals(userID) && x.PostId.Equals(PostId));

            if(rs == null)
            {
                status.isInteractive = false;
                status.isLike = false;
                status.isDislike = false;
                response.Code = "200";
                response.Message = "SUCCESS";
            }
            else
            {
                status.isInteractive = true;
                status.isLike = rs.IsLike;
                status.isDislike = rs.IsDisLike;
                response.Code = "200";
                response.Message = "SUCCESS";
            }
            if (rsSave != null)
            {
                status.isSave = true;
            }
            else
            {
                status.isSave = false;

            }

            response.Data = status;


            response.Code = "200";
            response.Message = "UNSUCCESS";
            return response;
        }

        public async Task<BaseResponse<string>> LikeOrDislike(Guid UserId, int PostId, bool IsLike)
        {
            BaseResponse<string> response = new();
            var post = await _context.Posts.FindAsync(PostId);
            var like = _context.Interactives.FirstOrDefault(x => x.UserId == UserId && x.PostId == PostId);
            if (post == null)
            {
                response.Code = "202";
                response.Message = "Post is not available";
                return response;
            }
            if (like == null)
            {
                like = new Interactive();
                like.UserId = UserId;
                like.IsLike = true;
                like.PostId = PostId;
                if (IsLike)
                {
                    int pLike = post.Like;
                    pLike++;
                    post.Like = pLike;
                    _context.Interactives.Add(like);
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                    response.Message = "Like successfully";
                    response.Code = "200";
                    return response;
                }
                else 
                {
                    response.Message = "No action ";
                    response.Code = "202";
                    return response;
                }
            }
            else
            {
                int pLike = post.Like;
                pLike--;
                post.Like = pLike;
                _context.Update(post);
                _context.Interactives.Remove(like);
                await _context.SaveChangesAsync();
                response.Message = "Dislike successfully";
                response.Code = "202";
                return response;
            }
        }
    }
}
