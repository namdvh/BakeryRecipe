using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Products;
using BakeryRecipe.ViewModels.Report;
using BakeryRecipe.ViewModels.Response;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Report
{
    public class ReportService : IReportService
    {
        private readonly BakeryDBContext _context;

        public ReportService(BakeryDBContext context)
        {
            _context = context;
        }

        public async Task<BasePagination<List<ReportDTO>>> GetAllReport(PaginationFilter filter)
        {
            BasePagination<List<ReportDTO>> response = new();

            List<ReportDTO> reportList = new();
            int totalRecords = 0;

            var orderBy = filter._order.ToString();

            if (string.IsNullOrEmpty(filter._by))
            {
                filter._by = "PostId";
            }


            orderBy = orderBy switch
            {
                "1" => "descending",
                "-1" => "ascending",
                _ => orderBy
            };



            var data = await _context.Reports.Select(x => x.PostId).Distinct()
                 .ToListAsync();



            totalRecords = await _context.Reports.Select(x => x.PostId).Distinct().CountAsync();


            if (data == null)
            {
                response.Code = "202";
                response.Message = "There aren't any report in DB";
                return response;
            }
            else
            {
                foreach (var x in data)
                {
                    reportList.Add(MapToDTO(x));
                }
            }

            response.Data = reportList;
            response.Message = "SUCCESS";
            response.Code = "200";

            double totalPages;

            if (filter._all == false)
            {
                totalPages = ((double)totalRecords / (double)filter.PageSize);
            }
            else
            {
                totalPages = 1;
            }

            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            response.CurrentPage = filter.PageNumber;

            response.PageSize = filter._all == false ? filter.PageSize : totalRecords;

            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;

            return response;
        }

        public async Task<BaseResponse<List<ReportDetailDTO>>> GetReportDetail(int postID)
        {
            BaseResponse<List<ReportDetailDTO>> response = new();
            var data = await _context.Reports.Where(x => x.PostId == postID).ToListAsync();
            List<ReportDetailDTO> reportDetailDTO = new();


            if (data == null)
            {
                response.Code = "202";
                response.Message = "There aren't any report in DB";
                return response;
            }
            else
            {
                foreach (var x in data)
                {
                    reportDetailDTO.Add(MapToDetailDTO(x));
                }
            }

            response.Data = reportDetailDTO;
            response.Message = "SUCCESS";
            response.Code = "200";

            return response;
        }

        public async Task<BaseResponse<string>> CreateReport(CreateReportRequest request)
        {
            BaseResponse<string> response = new();

            var postReport = await _context.Reports.FirstOrDefaultAsync(x => x.UserId.Equals(request.UserID) && x.PostId == request.PostID);

            if (postReport == null)
            {

                Data.Entities.Report report = new()
                {
                    Date = DateTime.Now,
                    PostId = request.PostID,
                    ReportProblem = request.ReportProblem,
                    UserId = request.UserID

                };

                await _context.Reports.AddAsync(report);
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
            }
            else
            {
                response.Message = "You have report this post already";
                response.Code = "202";
            }

            return response;
        }

        private ReportDTO MapToDTO(int postID)
        {
            var countReport = _context.Reports.Count(x => x.PostId == postID);
            var post = _context.Posts.Where(x => x.Id == postID).FirstOrDefault();


            ReportDTO dto = new ReportDTO()
            {
                Count = countReport,
                PostID = postID,
                Date = post.CreatedDate,
                Image = post.Image,
                Title = post.Title,
            };
            return dto;

        }

        private ReportDetailDTO MapToDetailDTO(Data.Entities.Report report)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == report.UserId);


            ReportDetailDTO dto = new ReportDetailDTO()
            {
                Date = report.Date,
                FullName = user.FirstName + " " + user.LastName,
                PostID = report.PostId,
                ReportProblem = report.ReportProblem

            };
            return dto;
        }




    }
}
