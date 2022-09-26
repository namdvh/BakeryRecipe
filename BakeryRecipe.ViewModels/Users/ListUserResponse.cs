using BakeryRecipe.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Users
{
    public class ListUserResponse
    {
        public IEnumerable<UserDTO> Content { get; set; }

        public string Code { get; set; }
        public string Message { get; set; }

        public PaginationDTO Pagination { get; set; }
    }
}
