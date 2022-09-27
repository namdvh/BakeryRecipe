using BakeryRecipe.Application.System.Users;
using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IConfiguration _config;
        private readonly BakeryDBContext _context;
        public CommentService(IConfiguration config, BakeryDBContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<BaseResponse<string>> AddComment(CommentRequestDTO request)
        {

            BaseResponse<string> response = new();
            var post = await _context.Posts.FindAsync(request.PostId);
            if (request.UserId == null && post == null)
            {
                response.Code = "404";
                response.Message = "UserID or post is not available";
            }
            if (request.Content == null)
            {
                response.Code = "404";
                response.Message = "Comment is null";
            }
            else
            {
                if (request.ReplyId != null)
                {
                    var comment = new Comment()
                    {
                        PostId = request.PostId,
                        UserId = request.UserId,
                        Content = request.Content,
                        ReplyToId = request.ReplyId
                    };
                    _context.Add(comment);
                    _context.SaveChangesAsync();
                }
                else
                {
                    var comment = new Comment()
                    {
                        PostId = request.PostId,
                        UserId = request.UserId,
                        Content = request.Content,
                        ReplyToId = null,
                    };
                    _context.Add(comment);
                    _context.SaveChangesAsync();
                }
                
                response.Code = "200";
                response.Message = "Comment successfully";
            }
            return response;
        }
    }
}
