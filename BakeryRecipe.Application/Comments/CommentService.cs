using BakeryRecipe.Application.System.Users;
using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Statictis;
using BakeryRecipe.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                    await _context.SaveChangesAsync();
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

        public async Task<BaseResponse<List<StaticstisDay>>> GetStaticCommentsDay()
        {
            BaseResponse<List<StaticstisDay>> response = new();
            var list = await _context.Comments.Where(x => x.CreatedDate.Year.Equals(DateTime.Now.Year) && x.CreatedDate.Month.Equals(DateTime.Now.Month)).GroupBy(u => u.CreatedDate.Day)
            .Select(u => new StaticstisDay
            {
                Count = u.Count(),
                Day = u.FirstOrDefault().CreatedDate.Day.ToString()
            }).ToListAsync();
            foreach (var postData in list)
            {
                switch (postData.Day)
                {
                    case "1":
                        postData.Day = "1";
                        break;
                    case "2":
                        postData.Day = "2";
                        break;
                    case "3":
                        postData.Day = "3";
                        break;
                    case "4":
                        postData.Day = "4";
                        break;
                    case "5":
                        postData.Day = "5";
                        break;
                    case "6":
                        postData.Day = "6";
                        break;
                    case "7":
                        postData.Day = "7";
                        break;
                    case "8":
                        postData.Day = "8";
                        break;
                    case "9":
                        postData.Day = "9";
                        break;
                    case "10":
                        postData.Day = "10";
                        break;
                    case "11":
                        postData.Day = "11";
                        break;
                    case "12":
                        postData.Day = "12";
                        break;
                    case "13":
                        postData.Day = "13";
                        break;
                    case "14":
                        postData.Day = "14";
                        break;
                    case "15":
                        postData.Day = "15";
                        break;
                    case "16":
                        postData.Day = "16";
                        break;
                    case "17":
                        postData.Day = "17";
                        break;
                    case "18":
                        postData.Day = "18";
                        break;
                    case "19":
                        postData.Day = "19";
                        break;
                    case "20":
                        postData.Day = "20";
                        break;
                    case "21":
                        postData.Day = "21";
                        break;
                    case "22":
                        postData.Day = "22";
                        break;
                    case "23":
                        postData.Day = "23";
                        break;
                    case "24":
                        postData.Day = "24";
                        break;
                    case "25":
                        postData.Day = "25";
                        break;
                    case "26":
                        postData.Day = "26";
                        break;
                    case "27":
                        postData.Day = "27";
                        break;
                    case "28":
                        postData.Day = "28";
                        break;
                    case "29":
                        postData.Day = "29";
                        break;
                    case "30":
                        postData.Day = "30";
                        break;
                    case "31":
                        postData.Day = "31";
                        break;
                    default:
                        postData.Day = "error";
                        break;
                }
            }

            response.Code = "200";
            response.Data = list;
            response.Code = "200";
            return response;
        }

        public async Task<BaseResponse<List<StaticstisMonth>>> GetStaticCommentsMonth()
        {
            BaseResponse<List<StaticstisMonth>> response = new();
            var list = await _context.Comments.Where(x => x.CreatedDate.Year.Equals(DateTime.Now.Year)).GroupBy(u => u.CreatedDate.Month)
            .Select(u => new StaticstisMonth
            {
                Count = u.Count(),
                Month = u.FirstOrDefault().CreatedDate.Month.ToString()
            }).ToListAsync();
            foreach (var postData in list)
            {
                switch (postData.Month)
                {
                    case "1":
                        postData.Month = "Jan";
                        break;
                    case "2":
                        postData.Month = "Feb";
                        break;
                    case "3":
                        postData.Month = "Mar";
                        break;
                    case "4":
                        postData.Month = "Apr";
                        break;
                    case "5":
                        postData.Month = "May";
                        break;
                    case "6":
                        postData.Month = "Jun";
                        break;
                    case "7":
                        postData.Month = "Jul";
                        break;
                    case "8":
                        postData.Month = "Aug";
                        break;
                    case "9":
                        postData.Month = "Sep";
                        break;
                    case "10":
                        postData.Month = "Oct";
                        break;
                    case "11":
                        postData.Month = "Nov";
                        break;
                    case "12":
                        postData.Month = "Dec";
                        break;
                    default:
                        postData.Month = "error";
                        break;
                }
            }

            response.Code = "200";
            response.Data = list;
            response.Code = "200";
            return response;
        }

        public async Task<BaseResponse<List<StaticstisYear>>> GetStaticCommentsYear()
        {
            var currentYear = DateTime.Now.Year;
            var oneYear = DateTime.Now.AddYears(-1).Year;
            var twoYear = DateTime.Now.AddYears(-2).Year;
            List<StaticstisYear> listResponse = new();

            BaseResponse<List<StaticstisYear>> response = new();
            int dataOneYear = await _context.Comments.Where(x => x.CreatedDate.Year.Equals(oneYear)).CountAsync();
            int current = await _context.Comments.Where(x => x.CreatedDate.Year.Equals(DateTime.Now.Year)).CountAsync();
            int dataTwoYear = await _context.Comments.Where(x => x.CreatedDate.Year.Equals(twoYear)).CountAsync();
            StaticstisYear dataCurrent = new StaticstisYear(current, currentYear.ToString());
            StaticstisYear oneYearData = new StaticstisYear(dataOneYear, oneYear.ToString());
            StaticstisYear twoYearData = new StaticstisYear(dataTwoYear, twoYear.ToString());

            listResponse.Add(dataCurrent);
            listResponse.Add(oneYearData);
            listResponse.Add(twoYearData);

            response.Code = "200";
            response.Data = listResponse;
            response.Code = "200";
            return response;

        }
    }
}
