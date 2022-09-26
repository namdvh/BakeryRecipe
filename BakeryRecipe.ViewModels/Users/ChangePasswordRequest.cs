using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Users
{
    public class ChangePasswordRequest
    {
        public Guid UserID { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPass { get; set; }
        public string? Code { get; set; }
    }
}
