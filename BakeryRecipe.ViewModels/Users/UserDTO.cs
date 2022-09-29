using BakeryRecipe.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Users
{
    public class UserDTO
    {

        public string Id { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DOB { get; set; }

        public string? Avatar { get; set; }


        public DateTime? CreatedDate { get; set; }

        public string? Role { get; set; }

        public Status? Status { get; set; }
    }
}
