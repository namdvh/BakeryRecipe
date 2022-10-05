using BakeryRecipe.Data.DataContext;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.PostProduct;
using BakeryRecipe.ViewModels.Posts;
using BakeryRecipe.ViewModels.Repost;
using BakeryRecipe.ViewModels.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Repost
{
    public class RepostService : IRepostService
    {
        private readonly BakeryDBContext _context;

        public RepostService(BakeryDBContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<string>> CreateRepost(CreateRepostRequest request)
        {
            BasePagination<string> response = new();
            Data.Entities.Repost repost = new()
            {
                Date = DateTime.Now,
                PostId = request.PostId,
                UserId = request.UserId
            };
            await _context.Reposts.AddAsync(repost);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Message = "SUCCESS";
                response.Code = "200";
            }
            else
            {
                response.Message = "UNSUCCESS";
                response.Code = "202";
            }
            return response;
        }
        public async Task<BaseResponse<List<RepostDetailDTO>>> GetRepostByUsID(Guid UserId)
        {
            BaseResponse<List<RepostDetailDTO>> response = new();
            var result =await _context.Reposts.Include(x => x.Post).Where(x => x.UserId.Equals(UserId)).ToListAsync();
            List<RepostDetailDTO> list = new();
            if (result != null)
            {

                foreach (var item in result)
                {
                    list.Add(MapToDTO(item));
                }
                response.Data=list;
                response.Message = "SUCCESS";
                response.Code = "200";
            }
            else
            {
                response.Message = "UNSUCCESS";
                response.Code = "202";
            }
            return response;
        }
        public async Task<bool> DeleteRepost(DeleteRepostRequest request)
        {
            var repost = await _context.Reposts.FirstOrDefaultAsync(x => x.PostId == request.RepostId && x.UserId.Equals(request.UserId));

            if (repost == null)
            {
                return false;
            }
            _context.Reposts.Remove(repost);
            await _context.SaveChangesAsync();


            return true;
        }

        private RepostDetailDTO MapToDTO(Data.Entities.Repost rp)
        {
            RepostDetailDTO dto = new()
            {
                UserId = rp.UserId,
                PostId = rp.PostId,
                CreatedDate=rp.Date,
                Post= GetProductFromPost(rp.PostId),
            };
            return dto;
        }
        private List<PostDTO> GetProductFromPost(int postID)
        {
            var results = _context.Posts.Include(x=>x.Author).Where(x => x.Id == postID).ToList();

            var final = new List<PostDTO>();
            PostDTO dto = new();

            foreach (var x in results)
            {
                dto.AuthorID = x.AuthorId;
                dto.AuthorAvatar = x.Author.Avatar;
                dto.AuthorName = x.Author.FirstName;
                dto.Content = x.Content;
                dto.Id = x.Id;
                dto.Image = x.Image;
                dto.Like = x.Like;
                dto.Title = x.Title;
                final.Add(dto);
            }
            return final;
        }
    }
}
