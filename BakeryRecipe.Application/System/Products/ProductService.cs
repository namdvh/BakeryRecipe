using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Products;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BakeryRecipe.ViewModels.Posts;

namespace BakeryRecipe.Application.System.Products
{
    public class ProductService : IProductService
    {
        private readonly BakeryDBContext _context;

        public ProductService(BakeryDBContext context)
        {
            _context = context;
        }

        public async Task<BasePagination<List<ProductDTO>>> GetAllProduct(PaginationFilter filter)
        {
            BasePagination<List<ProductDTO>> response = new();
            List<ProductDTO> productList = new();
            int totalRecords = 0;

            var orderBy = filter._order.ToString();

            if (string.IsNullOrEmpty(filter._by))
            {
                filter._by = "ProductId";
            }


            orderBy = orderBy switch
            {
                "1" => "descending",
                "-1" => "ascending",
                _ => orderBy
            };

            dynamic data;

            if (filter._all)
            {
                data = await _context.Products.OrderBy(filter._by + " " + orderBy).ToListAsync();


            }
            else
            {
                data = await _context.Products.OrderBy(filter._by + " " + orderBy)
                    .Where(x=>x.Status==Data.Enum.Status.ACTIVE)
                    .OrderBy(filter._by + " " + orderBy)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();



            }
            totalRecords = await _context.Products.Where(x => x.Status == Data.Enum.Status.ACTIVE).CountAsync();


            if (data == null)
            {
                response.Code = "202";
                response.Message = "There aren't any products in DB";
                return response;
            }
            else
            {
                foreach (var x in data)
                {
                    productList.Add(MapToDTO(x));
                }
            }

            response.Data = productList;
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


        private ProductDTO MapToDTO(Product product)
        {
            ProductDTO productDTO = new()
            {
              CreatedDate = product.CreatedDate,
              Price = product.Price,
              ProductId = product.ProductId,
              ProductImage = product.ProductImage,
              ProductName = product.ProductName,
              UnitInStock = product.UnitInStock,
              

            };
            return productDTO;
        }
    }
}
