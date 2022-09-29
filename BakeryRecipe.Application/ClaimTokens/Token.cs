using BakeryRecipe.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.ClaimTokens
{
    public class Token
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }

        public UserDTO User { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
        public Token(string code, string msg)
        {
            Code= code;
            Message = msg;
        }
        public Token(string accessToken, string refreshToken, UserDTO user,string code,string message)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            User = user;
            Code = code;
            Message = message;
        }

        public Token()
        {

        }
    }
}
