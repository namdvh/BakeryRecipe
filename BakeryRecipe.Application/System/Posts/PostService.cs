using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.Data.Enum;
using BakeryRecipe.ViewModels.Comments;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.PostProduct;
using BakeryRecipe.ViewModels.Posts;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Posts
{
    public class PostService : IPostService
    {
        private readonly BakeryDBContext _context;

        public PostService(BakeryDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePost(AddPostRequest request)
        {
            bool flag = false;
            PostProduct postProduct = new();
            int rsPost = 0;


            var post = new Post
            {
                AuthorId = request.AuthorID,
                CategoryId = request.CategoryID,
                Content = request.Content,
                Image = request.Image,
                Like = 0,
                Status = request.Status,
                Title = request.Title,

            };

            _context.Posts.Add(post);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                foreach (var x in request.PostProduct)
                {
                    postProduct.ProductId = x.ProductID;
                    postProduct.PostId = post.Id;
                    postProduct.Quantity = x.Quantity;

                    _context.PostProducts.Add(postProduct);
                    rsPost = await _context.SaveChangesAsync();
                }
            }

            if (rsPost > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<BasePagination<List<PostDTO>>> GetPost(PaginationFilter filter)
        {
            BasePagination<List<PostDTO>> response = new();
            List<PostDTO> postList = new();

            if (string.IsNullOrEmpty(filter._by))
            {
                filter._by = "Id";
            }

            var orderBy = filter._order.ToString();


            orderBy = orderBy switch
            {
                "1" => "descending",
                "-1" => "ascending",
                _ => orderBy
            };


            var data = await _context.Posts
                .Where(x => x.Status.Equals(Status.ACTIVE))
                .OrderBy(filter._by + " " + orderBy)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();

            var totalRecords = await _context.Posts.Where(x => x.Status == Status.ACTIVE).CountAsync();


            if (!data.Any())
            {
                response.Data = null;
                response.Code = "202";
                response.Message = "There aren't any post in DB";
                return response;

            }
            else
            {
                foreach (var x in data)
                {
                    postList.Add(MapToDTO(x));
                }

            }

            double totalPages;

            totalPages = ((double)totalRecords / (double)filter.PageSize);

            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            response.CurrentPage = filter.PageNumber;

            response.PageSize = filter.PageSize;
            response.Data = postList;
            response.Code = "200";
            response.Message = "SUCCESS";
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;

            return response;
        }

        public async Task<BaseResponse<PostDetailDTO>> GetDetailPost(int id)
        {
            BaseResponse<PostDetailDTO> response = new();
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);


            if (post == null)
            {
                response.Code = "202";
                response.Message = "Cannot find that Post";
                return response;
            }

            PostDetailDTO postDetail = MapToDetailDTO(post);
            response.Data = postDetail;
            response.Code = "200";
            response.Message = "SUCCESS";
            return response;


        }

        public async Task<bool> UpdatePost(UpdatePostRequest request, int postID)
        {
            var flag = false;
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postID);
            var postCategories = new PostProduct();
            var postProduct = _context.PostProducts.Where(x => x.PostId == postID).Select(x => x.ProductId).ToList();
            //var postQuantity = _context.PostProducts.Where(x => x.PostId == postID).ToList();

            var products = await _context.Products.ToListAsync();


            if (post != null)
            {
                post.Title = request.Title;
                post.Content = request.Content;
                post.Image = request.Image;
                post.CategoryId = request.CategoryID;


                for (int i = 0; i < products.Count; i++)
                {
                    if (request.PostProduct.Select(x => x.ProductID).Contains(products[i].ProductId))       //is checked
                    {
                        if (!postProduct.Contains(products[i].ProductId))
                        {
                            postCategories = new()
                            {
                                ProductId = products[i].ProductId,
                                PostId = post.Id
                            };
                            var rss = request.PostProduct.FirstOrDefault(x => x.ProductID == products[i].ProductId);
                            if (rss != null)
                            {

                                postCategories.Quantity = rss.Quantity;

                            }
                            _context.PostProducts.Add(postCategories);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        if (postProduct.Contains(products[i].ProductId))
                        {
                            postCategories = new()
                            {
                                ProductId = products[i].ProductId,
                                PostId = post.Id
                            };
                            _context.PostProducts.Remove(postCategories);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                var currentPostProduct = await _context.PostProducts.Where(x => x.PostId == postID).ToListAsync();



                for (int i = 0; i < currentPostProduct.Count; i++)
                {
                    for (int y = 0; y < request.PostProduct.Count; y++)
                    {
                        if (currentPostProduct[i].ProductId == request.PostProduct[y].ProductID)
                        {
                            var postQuantity = _context.PostProducts
                                .FirstOrDefault(x => x.PostId == currentPostProduct[i].PostId && x.ProductId == currentPostProduct[i].ProductId);
                            if (postQuantity.Quantity != request.PostProduct[i].Quantity)
                            {
                                postQuantity.Quantity = request.PostProduct[i].Quantity;
                                _context.PostProducts.Update(postQuantity);
                                await _context.SaveChangesAsync();
                            }

                        }
                    }
                }


                _context.Posts.Update(post);
                var rs = await _context.SaveChangesAsync();

                if (rs > 0)
                {
                    return true;
                }


            }
            return false;


        }

        private PostDTO MapToDTO(Post post)
        {
            var user = _context.Users.Find(post.AuthorId);
            var category = _context.Categories.Find(post.CategoryId);

            PostDTO postDTO = new()
            {
                AuthorAvatar = user.Avatar,
                AuthorID = post.AuthorId,
                AuthorName = user.FirstName + " " + user.LastName,
                CategoryID = category.CategoryId,
                CategoryName = category.CategoryName,
                Content = post.Content,
                Id = post.Id,
                Image = post.Image,
                Like = post.Like,
                Title = post.Title

            };
            return postDTO;
        }

        private PostDetailDTO MapToDetailDTO(Post post)
        {
            var user = _context.Users.Find(post.AuthorId);
            var category = _context.Categories.Find(post.CategoryId);

            PostDetailDTO postDTO = new()
            {
                AuthorAvatar = user.Avatar,
                AuthorID = post.AuthorId,
                AuthorName = user.FirstName + " " + user.LastName,
                CategoryID = category.CategoryId,
                CategoryName = category.CategoryName,
                Content = post.Content,
                Id = post.Id,
                Image = post.Image,
                Like = post.Like,
                Title = post.Title,
                PostProducts = GetProductFromPost(post.Id),
                Comments = GetCommentFromPost(post.Id),

            };
            return postDTO;
        }

        private List<PostProductDTO> GetProductFromPost(int postID)
        {
            var results = _context.PostProducts.Include(x => x.Product).Where(x => x.PostId == postID).ToList();

            var final = new List<PostProductDTO>();
            PostProductDTO dto = new();

            foreach (var x in results)
            {
                dto.ProductName = x.Product.ProductName;
                dto.ProductID = x.ProductId;
                dto.Quantity = x.Quantity;
                dto.Type = x.Product.UnitType;
                final.Add(dto);
            }

            return final;
        }

        private List<CommentPostDTO> GetCommentFromPost(int postID)
        {
            var results = _context.Comments
                .Include(x => x.User)
                .Where(x => x.PostId == postID && x.ReplyToId.Equals(null))
                .ToList();

            var final = new List<CommentPostDTO>();

            foreach (var x in results)
            {
                CommentPostDTO dto = new();
                dto.CommentId = x.CommentId;
                dto.UserId = x.UserId;
                dto.UserName = x.User.FirstName + " " + x.User.LastName;
                dto.Avatar = x.User.Avatar;
                dto.Content = x.Content;
                dto.ReplyTo = GetReplyFromPost(x.CommentId);
                final.Add(dto);
            }

            return final;
        }


        private List<CommentPostDTO> GetReplyFromPost(int CommentId)
        {
            var results = _context.Comments
                .Include(x => x.User)
                .Where(x => x.ReplyToId == CommentId)
                .ToList();

            var final = new List<CommentPostDTO>();

            foreach (var x in results)
            {
                CommentPostDTO dto = new();
                dto.CommentId = x.CommentId;
                dto.UserId = x.UserId;
                dto.UserName = x.User.FirstName + " " + x.User.LastName;
                dto.Avatar = x.User.Avatar;
                dto.Content = x.Content;
                final.Add(dto);
            }

            return final;
        }

        public async Task<bool> DeletePost(int postID)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postID);

            if (post == null)
            {
                return false;
            }

            if (post.Status == Status.INACTIVE)
            {
                post.Status = Status.ACTIVE;
            }
            else
            {
                post.Status = Status.INACTIVE;
            }

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();


            return true;
        }

        public async Task<BasePagination<List<PostDTO>>> SearchPostByCategories(PaginationFilter filter, int categoriesID)
        {
            BasePagination<List<PostDTO>> response = new();
            List<PostDTO> postList = new();

            if (string.IsNullOrEmpty(filter._by))
            {
                filter._by = "Id";
            }

            var orderBy = filter._order.ToString();


            orderBy = orderBy switch
            {
                "1" => "descending",
                "-1" => "ascending",
                _ => orderBy
            };


            var data = await _context.Posts
                .Where(x => x.Status.Equals(Status.ACTIVE) && x.CategoryId == categoriesID)
                .OrderBy(filter._by + " " + orderBy)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();

            var totalRecords = await _context.Posts.Where(x => x.Status == Status.ACTIVE && x.CategoryId == categoriesID).CountAsync();


            if (!data.Any())
            {
                response.Data = null;
                response.Code = "202";
                response.Message = "There aren't any post match that keyword in DB";
                return response;
            }
            else
            {
                foreach (var x in data)
                {
                    postList.Add(MapToDTO(x));
                }

            }

            double totalPages;

            totalPages = ((double)totalRecords / (double)filter.PageSize);

            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            response.CurrentPage = filter.PageNumber;

            response.PageSize = filter.PageSize;
            response.Data = postList;
            response.Code = "200";
            response.Message = "SUCCESS";
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;

            return response;
        }

        public async Task<BasePagination<List<PostDTO>>> SearchPostByName(PaginationFilter filter, string keyword)
        {
            BasePagination<List<PostDTO>> response = new();
            List<PostDTO> postList = new();

            if (string.IsNullOrEmpty(filter._by))
            {
                filter._by = "Id";
            }

            var orderBy = filter._order.ToString();


            orderBy = orderBy switch
            {
                "1" => "descending",
                "-1" => "ascending",
                _ => orderBy
            };


            var data = await _context.Posts
                .Where(x => x.Status.Equals(Status.ACTIVE) && x.Title.Contains(keyword))
                .OrderBy(filter._by + " " + orderBy)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();

            var totalRecords = await _context.Posts.Where(x => x.Status == Status.ACTIVE && x.Title.Contains(keyword)).CountAsync();


            if (!data.Any())
            {
                response.Data = null;
                response.Code = "202";
                response.Message = "There aren't any post match that keyword in DB";
                return response;

            }
            else
            {
                foreach (var x in data)
                {
                    postList.Add(MapToDTO(x));
                }

            }

            double totalPages;

            totalPages = ((double)totalRecords / (double)filter.PageSize);

            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            response.CurrentPage = filter.PageNumber;

            response.PageSize = filter.PageSize;
            response.Data = postList;
            response.Code = "200";
            response.Message = "SUCCESS";
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;

            return response;
        }

        public async Task<BasePagination<List<PostDTO>>> GetPostByStatusAndUserID(PaginationFilter filter, Status status, Guid userID)
        {
            BasePagination<List<PostDTO>> response = new();
            List<PostDTO> postList = new();

            if (string.IsNullOrEmpty(filter._by))
            {
                filter._by = "Id";
            }

            var orderBy = filter._order.ToString();


            orderBy = orderBy switch
            {
                "1" => "descending",
                "-1" => "ascending",
                _ => orderBy
            };


            var data = await _context.Posts
                .Where(x => x.Status == status && x.AuthorId.Equals(userID))
                .OrderBy(filter._by + " " + orderBy)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();

            var totalRecords = await _context.Posts.Where(x => x.Status == Status.ACTIVE).CountAsync();


            if (!data.Any())
            {
                response.Data = null;
                response.Code = "202";
                response.Message = "There aren't any post in DB";
                return response;

            }
            else
            {
                foreach (var x in data)
                {
                    postList.Add(MapToDTO(x));
                }

            }

            double totalPages;

            totalPages = ((double)totalRecords / (double)filter.PageSize);

            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            response.CurrentPage = filter.PageNumber;

            response.PageSize = filter.PageSize;
            response.Data = postList;
            response.Code = "200";
            response.Message = "SUCCESS";
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;

            return response;
        }

        public async Task<BaseResponse<List<StaticstisPost>>> GetStaticPostMonth()
        {
            BaseResponse<List<StaticstisPost>> response = new();
            var list = await _context.Posts.Where(x => x.CreatedDate.Year.Equals(DateTime.Now.Year)).GroupBy(u => u.CreatedDate.Month)
            .Select(u => new StaticstisPost
            {
                PostCount = u.Count(),
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

        public async Task<BaseResponse<List<StaticstisPostYear>>> GetStaticPostYear()
        {
            var currentYear = DateTime.Now.Year;
            var oneYear = DateTime.Now.AddYears(-1).Year;
            var twoYear = DateTime.Now.AddYears(-2).Year;
            int count = 3;
            List<StaticstisPostYear> listResponse = new();

            BaseResponse<List<StaticstisPostYear>> response = new();
            int dataOneYear = await _context.Posts.Where(x => x.CreatedDate.Year.Equals(oneYear)).CountAsync();
            int current = await _context.Posts.Where(x => x.CreatedDate.Year.Equals(DateTime.Now.Year)).CountAsync();
            int dataTwoYear = await _context.Posts.Where(x => x.CreatedDate.Year.Equals(twoYear)).CountAsync();
            StaticstisPostYear dataCurrent = new StaticstisPostYear(current, currentYear.ToString());
            StaticstisPostYear oneYearData = new StaticstisPostYear(dataOneYear, oneYear.ToString());
            StaticstisPostYear twoYearData = new StaticstisPostYear(dataTwoYear, twoYear.ToString());

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
