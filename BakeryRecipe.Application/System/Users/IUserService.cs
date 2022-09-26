using BakeryRecipe.Application.ClaimTokens;
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

        //Task<ListUserResponse> GetUserList(PaginationFilter filter);
        Task<UserDTO> GetUser(Guid userId);
    }
}
