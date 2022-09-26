using BakeryRecipe.Application.ClaimTokens;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Users
{
    public interface IUserService
    {
        Task<Token> Login(LoginRequestDTO request);
        Task<RefreshTokenResponse> RefreshToken(RefreshTokenResponse refreshToken);
        Task<RegisterResponseDTO> Register(RegisterRequestDTO request);
        Task<ProfileResponseDTO> GetProfile(RefreshTokenResponse refreshToken);
        Task<bool> SendEmail(string email,string code);

        Task<BaseResponse<string>> ChangePassword(Guid id,string currentPass,string newPass);
        Task<bool> ForgotPassword(Guid userID, string newPass);

        //Task<ListUserResponse> GetUserList(PaginationFilter filter);
        Task<UserDTO> GetUser(Guid userId);
    }
}
