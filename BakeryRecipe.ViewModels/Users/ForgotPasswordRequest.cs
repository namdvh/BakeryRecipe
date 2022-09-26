using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Users
{
    public class ForgotPasswordRequest
    {
        public Guid UserId { get; set; }

        public string NewPassword { get; set; }    =string.Empty;

        public string? Code { get; set; }
        
    }
}
