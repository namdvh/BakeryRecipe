﻿using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.Data.Enum;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BakeryDBContext _context;

        public CategoryService(BakeryDBContext context)
        {
            _context = context;
        }

        public async Task<BasePagination<List<Category>>> GetAllCategories(PaginationFilter filter)
        {
            BasePagination<List<Category>> response = new();
            int totalRecords = 0;

            var orderBy = filter._order.ToString();

            if (string.IsNullOrEmpty(filter._by))
            {
                filter._by = "CategoryId";
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
                data = await _context.Categories.OrderBy(filter._by + " " + orderBy).ToListAsync();


            }
            else
            {
                data = await _context.Categories.OrderBy(filter._by + " " + orderBy)
                    .OrderBy(filter._by + " " + orderBy)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();



            }
            totalRecords = await _context.Categories.CountAsync();


            if (data == null)
            {
                response.Code = "202";
                response.Message = "There aren't any category in DB";
                return response;
            }
            
                response.Content = data;
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
    }
}
