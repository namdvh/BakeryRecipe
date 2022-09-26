using BakeryRecipe.Application.ClaimTokens;
using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.Data.Enum;
using BakeryRecipe.ViewModels.Users;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Users
{
    public class UserService:IUserService
    {
        private readonly UserManager<User> _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        private readonly BakeryDBContext _context;
        public UserService(UserManager<User> userService, SignInManager<User> signInService, RoleManager<Role> roleManager, IConfiguration config, BakeryDBContext context)
        {
            _userService = userService;
            _signInManager = signInService;
            _roleManager = roleManager;
            _config = config;
            _context = context;
        }
        public async Task<Token> Login(LoginRequestDTO request)
        {
            var user = await _userService.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return null;
            }
            var rs = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!rs.Succeeded)
            {
                return null;
            }

            var roleName = (from usr in _context.Users
                            join userRole in _context.UserRoles on user.Id equals userRole.UserId
                            join role in _context.Roles on userRole.RoleId equals role.Id
                            select role.Name).FirstOrDefault();

            var roles = await _userService.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role,string.Join(";",roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accesstoken = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            var refreshtoken = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);
            var ReturnToken = new JwtSecurityTokenHandler().WriteToken(accesstoken);
            var ReturnRFToken = new JwtSecurityTokenHandler().WriteToken(refreshtoken);
            //save rf token to db
            user.Token = ReturnRFToken;
            user.RefreshTokenExpiryTime = refreshtoken.ValidTo;
            await _userService.UpdateAsync(user);

            var userDto = MapToDto(user, roleName);

            var token = new Token(ReturnToken, ReturnRFToken, userDto);

            return token;
        }
        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenResponse refreshToken)
        {
            RefreshTokenResponse response = new RefreshTokenResponse();
            var tokenHandler = new JwtSecurityTokenHandler();


            var principal = GetPrincipalFromToken(refreshToken.RefreshToken);

            try
            {

                if (GetPrincipalFromToken(refreshToken.RefreshToken) == null)
                {
                    response.Code = "900";
                    response.Message = "Invalid Token";
                    return response;
                }
            }
            catch (SecurityTokenExpiredException)
            {

                response.Code = "901";
                response.Message = "Expired Token";
                return response;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                response.Code = "900";
                response.Message = "Invalid Token";
                return response;
            }
            catch (ArgumentException)
            {
                response.Code = "900";
                response.Message = "Arugment invalid Token";
                return response;
            }


            string username = principal.Identity.Name;
            var user = await _userService.FindByNameAsync(username);

            if (user == null || user.Token != refreshToken.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                response.Code = "901";
                response.Message = "Expired Token";
                response.AccessToken = "";
                response.RefreshToken = "";
                return response;
            }
            var roles = await _userService.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role,string.Join(";",roles))
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var accesstoken = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);
            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(accesstoken);
            response.AccessToken = newAccessToken;
            response.Code = "200";
            response.Message = "Generate new token successfully";
            return response;
        }
        private ClaimsPrincipal GetPrincipalFromToken(string? token)

        {
            var principal = (dynamic)null;
            try
            {
                string issuer = _config.GetValue<string>("Tokens:Issuer");
                string signingKey = _config.GetValue<string>("Tokens:Key");
                byte[] signingKeyBytes = Encoding.UTF8.GetBytes(signingKey);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes),
                };
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            }
            catch (SecurityTokenExpiredException ex)
            {
                throw new SecurityTokenExpiredException();
            }
            catch (SecurityTokenInvalidSignatureException e)
            {
                throw new SecurityTokenInvalidSignatureException();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException();
            }
            return principal;
        }
        public async Task<RegisterResponseDTO> Register(RegisterRequestDTO request)
        {
            RegisterResponseDTO response = new RegisterResponseDTO();
            response.Messages = new();
            RegisterRequestValidatorDTO validator = new ();
            ValidationResult results = validator.Validate(request);

            var defaultRole = _roleManager.FindByIdAsync("dc48ba58-ddcb-41de-96fe-e41327e5f313").Result;

            if (!results.IsValid)
            {

                response.Content = null;
                response.Code = "202";
                foreach (var failure in results.Errors)
                {
                    response.Messages.Add(failure.ErrorMessage.ToString());
                }
                return response;
            }
            else
            {
                var user = new User()
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    FirstName=request.FirstName,
                    LastName=request.LastName,
                    CreatedDate=request.CreateDate,
                    Status = Status.ACTIVE
                };

                var rs = await _userService.CreateAsync(user, request.Password);
                if (rs.Succeeded)
                {

                    await _userService.AddToRoleAsync(user, defaultRole.Name);
                    response.Content = user;
                    response.Code = "200";
                    response.Messages.Add("Regist successfully");

                    return response;
                }
                response.Content = null;
                response.Code = "400";
                response.Messages.Add("Username already exists ");

                return response;
            }
        }
        private UserDTO MapToDto(User user, string roleName)
        {
            var userDto = new UserDTO()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender=user.Gender,
                Phone = user.PhoneNumber,
                Status = user.Status,
                Role = roleName,
                Avatar=user.Avatar
            };
            return userDto;
        }

        public Task<ProfileResponseDTO> GetProfile(RefreshTokenResponse refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
