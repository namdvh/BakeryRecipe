using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Users
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = string.Empty;

        public string NewPassword { get; set; }    =string.Empty;

        public string? Code { get; set; }
        
    }
}
