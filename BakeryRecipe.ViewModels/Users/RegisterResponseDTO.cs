using BakeryRecipe.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Users
{
    public class RegisterResponseDTO
    {
        public User Data { get; set; }

        public string Code { get; set; }

        public string? Messages { get; set; }
    }
}
