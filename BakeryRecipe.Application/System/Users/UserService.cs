﻿using BakeryRecipe.Application.ClaimTokens;
using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.Data.Enum;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Users;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BakeryRecipe.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        private readonly BakeryDBContext _context;
        private readonly string emailApi = "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";


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
            dynamic rs;
            if (request.UserName != null)
            {
                if (await _userService.FindByNameAsync(request.UserName) == null)
                {
                    var tokens = new Token("202", "Invalid Username or password");
                    return tokens;
                }
                var user = await _userService.FindByNameAsync(request.UserName);
                rs = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
                if (user == null)
                {
                    return null;
                }
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
                new Claim(ClaimTypes.Name,user.UserName),
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
                var Code = "200";
                var msg = "Login success";

                var token = new Token(ReturnToken, ReturnRFToken, userDto, Code, msg);

                return token;

            }
            else
            {
                if (await _userService.FindByEmailAsync(request.Email) == null)
                {
                    var tokens = new Token("202", "Invalid Email or password");
                    return tokens;
                }
                var user = await _userService.FindByEmailAsync(request.Email);
                rs = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
                if (user == null)
                {
                    return null;
                }
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
                var Code = "200";
                var msg = "Login success";
                var token = new Token(ReturnToken, ReturnRFToken, userDto, Code, msg);

                return token;

            }
            //var us = await _userService.FindByEmailAsync(request.Email);


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
            //response.Messages = new();
            RegisterRequestValidatorDTO validator = new();
            ValidationResult results = validator.Validate(request);

            var defaultRole = _roleManager.FindByIdAsync("dc48ba58-ddcb-41de-96fe-e41327e5f313").Result;

            if (!results.IsValid)
            {

                response.Data = null;
                response.Code = "202";
                foreach (var failure in results.Errors)
                {
                    response.Messages = failure.ErrorMessage.ToString();
                }
                return response;
            }
            else
            {
                var user = new User()
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    CreatedDate = request.CreateDate,
                    Status = Status.ACTIVE
                };

                var rs = await _userService.CreateAsync(user, request.Password);
                if (rs.Succeeded)
                {

                    await _userService.AddToRoleAsync(user, defaultRole.Name);
                    response.Data = user;
                    response.Code = "200";
                    response.Messages = "Regist successfully";

                    return response;
                }
                response.Data = null;
                response.Code = "400";
                response.Messages = "Username already exists ";

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
                Gender = user.Gender,
                Phone = user.PhoneNumber,
                Status = user.Status,
                Role = roleName,
                Avatar = user.Avatar
            };
            return userDto;
        }
        private UserDTO MapToDTO(User user)
        {
            UserDTO dto = new();
            dto.Email = user.Email;
            dto.Gender = user.Gender;
            dto.Id = user.Id.ToString();
            dto.Phone = user.PhoneNumber;
            dto.DOB = user.DOB;
            dto.Status = user.Status;
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.Role = "User";
            dto.Avatar = user.Avatar;
            return dto;
        }

        public Task<UserDTO> GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendEmail(string email, string code)
        {
            var client = new SendGridClient(emailApi);
            var from = new EmailAddress("bakeryrecipee@gmail.com", "Bakery Company");
            var subject = "Verification Email";
            var to = new EmailAddress(email);
            var plainTextContent = "This is your verification code";
            var htmlContent =


            "<div style=\"width:100%;max-width:440px;padding:0 20px\"><div class=\"adM\">\r\n    </div><div style=\"width:100%;padding:10px 7px\"><div class=\"adM\">\r\n      </div><svg\r\n    version=\"1.1\"\r\n    id=\"Layer_1\"\r\n    xmlns=\"http://www.w3.org/2000/svg\"\r\n    width=150px\r\n    height=100px\r\n    viewBox=\"0 0 130 46\"\r\n  >\r\n    <path\r\n      id=\"Logotype_4_\"\r\n      className=\"st0\"\r\n      d=\"M120.591 41.4552L120.298 40.9552C120.207 40.7734 120.162 40.6597 120.162 40.6597H120.14C120.14 40.6597 120.162 40.7734 120.162 40.9779V41.887H119.779V40.0234H120.185L120.568 40.7279C120.659 40.9097 120.681 40.9779 120.681 40.9779H120.704C120.704 40.9779 120.726 40.887 120.817 40.7279L121.2 40.0234H121.606V41.887H121.223V40.9779C121.223 40.7507 121.245 40.6597 121.245 40.6597H121.223C121.223 40.6597 121.178 40.7734 121.087 40.9552L120.794 41.4552H120.591ZM118.606 41.887V40.3416H118.087V40.0007H119.508V40.3416H118.989V41.887H118.606ZM93.1561 4.0234C92.389 3.47794 92.2085 2.40976 92.2987 1.40976C92.3664 0.614308 92.5018 -0.112965 93.4945 0.0233987C94.5774 0.18249 95.2768 0.523399 95.5025 2.06885C95.683 3.25067 95.1866 4.47794 94.0585 4.34158C93.7427 4.29612 93.4268 4.22794 93.1561 4.0234ZM73.5275 17.2052C71.4067 20.2734 67.2553 24.637 65.3602 25.2734C64.9089 25.4325 64.4803 25.4325 64.2321 25.137C63.9613 24.7961 63.8936 24.137 64.0516 23.2507C64.39 21.3416 65.3376 19.1825 66.9169 16.9098C67.8645 15.5461 69.4664 13.3416 71.2262 11.7734C72.7604 10.4098 74.5653 9.54612 76.0092 9.0234C76.7312 8.7734 77.1148 8.7734 77.2952 8.93249C77.6562 9.2734 77.5209 9.59158 77.2952 10.3189C76.8666 11.8188 75.3098 14.637 73.5275 17.2052ZM128.894 23.2734C128.465 23.7734 127.901 24.4779 126.953 25.4552C126.164 26.2961 125.126 27.1598 124.291 27.5688C123.366 28.0234 122.712 27.7734 122.261 27.2279C121.155 25.8416 120.997 21.887 121.042 18.4552C121.065 16.8188 121.29 15.2507 121.651 12.4098C121.99 9.75067 121.403 8.81885 118.718 8.75067C116.395 8.70521 114.567 10.0461 113.055 11.5688C112.536 12.1143 112.04 12.6598 111.589 13.1825C110.799 14.1143 109.987 15.2279 109.265 16.1143C109.062 16.3643 107.956 17.7507 107.438 18.4325C107.235 18.7052 106.986 19.0916 106.761 19.0461C106.513 18.9779 106.422 18.6143 106.422 18.137C106.377 17.2507 106.625 15.0916 106.874 13.6825C107.009 12.8188 107.077 12.2734 107.212 11.6598C107.302 11.2734 107.415 10.5689 107.234 10.2052C107.077 9.88703 106.806 9.59158 106.558 9.5234C105.926 9.31885 105.475 9.68249 105.294 10.2734C104.866 11.6143 104.189 13.5234 103.692 14.9098C102.632 17.887 101.526 20.3416 100.15 22.5007C98.3452 25.3416 95.6153 28.4779 93.9231 29.9098C92.9304 30.7279 91.7572 31.4779 90.8548 31.0688C89.9298 30.6597 89.7944 29.4098 89.7493 28.2734C89.7041 27.1825 90.0877 23.6825 90.6292 20.7961C91.2158 17.7052 92.2085 14.2052 93.0658 11.1825C93.2237 10.637 93.2689 9.93249 93.0207 9.31885C92.8628 8.95521 92.6146 8.6143 92.2085 8.3643C91.1481 7.70521 90.6743 8.68249 90.4261 9.45521C89.3206 12.7279 87.809 16.4552 86.5455 19.5461C85.1692 22.9098 82.71 26.1825 81.063 27.6143C80.4313 28.1825 79.7093 28.7734 79.1227 28.8643C78.5813 28.9552 78.2203 28.7052 78.0398 28.4779C77.7916 28.1825 77.6337 27.7279 77.5434 26.9779C77.4081 25.7961 77.4306 23.2961 78.0849 20.4552C78.9197 16.8188 80.3636 12.7734 81.0179 10.5007C81.424 9.06885 81.3338 7.79612 79.3935 6.88703C78.3556 6.40976 76.5282 6.54612 75.2421 6.81885C73.0085 7.31885 70.8426 8.59158 68.7895 9.97794C67.3005 10.9779 65.8339 12.5461 64.1644 14.5234C63.826 14.9325 63.0589 16.1143 62.3143 17.3188C61.4119 18.7734 60.532 20.2052 60.1259 21.7507C59.3813 24.5461 59.6972 26.4552 60.0582 27.5916C60.2838 28.2961 60.893 29.2734 61.66 29.7734C62.6076 30.387 63.6229 30.387 64.4802 30.1143C66.0144 29.5916 67.8194 28.1598 68.8572 27.2734C71.2262 25.2507 72.828 23.0916 73.9561 21.4098C74.2269 21.0007 74.475 20.5916 74.7458 20.1825C74.8586 20.0234 75.0165 20.0688 74.994 20.2734C74.9488 20.5916 74.9263 20.887 74.8586 21.7507C74.7458 23.2279 74.633 25.7279 74.8135 27.4552C74.9488 28.6825 75.197 30.0007 76.0769 30.9552C76.4605 31.3643 77.1373 31.8643 78.0172 32.0234C79.6868 32.3188 81.221 31.0234 82.349 30.1597C83.7479 29.0916 85.1918 27.3643 86.4101 25.887C86.6132 25.637 86.8388 25.7961 86.8388 26.0688C86.8388 27.2961 86.8614 28.6825 87.0419 29.7052C87.29 31.0916 88.0346 32.9779 89.2529 33.6143C91.0127 34.5234 92.9079 33.7279 95.0061 32.1597C96.1116 31.3416 97.8489 29.4779 98.6611 28.4098C99.8794 26.7961 101.075 25.2734 101.707 24.3188C101.91 24.0007 102.09 23.7507 102.271 23.7961C102.497 23.8643 102.451 24.0688 102.361 24.6143C102.181 25.9098 101.752 28.0234 101.617 29.9552C101.572 30.5916 101.594 31.1597 102.248 31.4779C102.497 31.5916 102.858 31.7052 103.151 31.7052C103.557 31.7279 103.895 31.5007 104.121 31.0234C104.414 30.4097 104.662 29.5688 104.911 29.0007C105.745 26.9552 106.106 26.0234 107.189 24.1143C108.25 22.2279 109.491 20.4552 110.867 18.7961C112.582 16.6825 113.935 15.0688 116.191 13.4779C116.598 13.1825 117.116 13.0234 117.365 13.1598C117.613 13.2961 117.613 13.7507 117.613 14.2507C117.545 16.0007 117.545 17.9325 117.59 19.637C117.703 23.0234 118.177 25.6825 118.809 27.5234C119.26 28.8188 120.343 30.9097 121.854 31.1143C123.163 31.2961 124.291 30.7961 125.803 29.5234C126.931 28.5916 127.856 27.3643 128.6 26.2279C128.984 25.6598 129.638 24.3416 129.819 23.9552C130.067 23.387 130.112 22.9779 129.796 22.8643C129.48 22.7507 129.277 22.8188 128.894 23.2734ZM20.0113 27.7279C19.1991 27.9552 15.4087 29.0007 12.1824 30.0461C10.919 30.4552 5.7975 32.2734 4.19563 32.7279C3.38341 32.9552 2.48095 32.9325 1.87178 32.7734C1.10469 32.5916 0.495524 32.0234 0.224784 31.5007C-0.181324 30.6598 0.02173 29.5688 0.1571 28.7507C0.630893 25.7052 1.42055 23.387 2.25533 20.2961C2.9773 17.6143 3.72183 14.7734 4.28587 12.2961C4.84991 9.8643 5.95543 6.11431 6.42923 5.00067C6.63228 4.5234 7.10608 3.7734 7.89573 3.50067C8.66283 3.22794 9.13662 3.25067 9.56529 3.38703C10.2647 3.59158 10.5806 4.00067 10.3098 4.95521C9.94884 6.15976 9.45248 7.59158 9.18174 8.56885C8.27928 11.8643 7.39938 14.6825 6.29386 18.8188C5.70726 21.0007 4.39868 26.2507 3.8572 28.9779C3.81208 29.1598 3.99257 29.3188 4.1505 29.2507C5.45908 28.8416 8.73051 27.7507 10.7611 27.2734C12.8593 26.7961 14.6191 26.4098 16.2435 26.1598C17.2137 26.0234 18.9284 25.7961 19.8985 25.8188C20.2595 25.8188 20.6656 25.8643 20.9363 26.2052C21.0266 26.3188 21.162 26.5916 21.1845 26.7507C21.3425 27.387 20.8235 27.5007 20.0113 27.7279ZM60.6673 12.3416C60.171 13.5007 59.449 15.0688 58.7947 16.2279C55.8617 21.4552 50.4244 29.9779 48.0328 33.0234C47.4462 33.7507 46.8145 34.5688 46.0925 34.5688C45.7541 34.5688 45.348 34.4552 45.1224 34.2507C44.5809 33.7507 44.5583 32.887 44.5809 31.5234C44.6035 27.5007 44.5132 23.2279 44.423 20.4779C44.3553 18.0007 44.1748 15.8643 43.9943 13.8643C43.9041 12.887 43.7687 11.5688 43.9943 10.7279C44.1522 10.0916 44.5809 9.2734 45.0773 8.63703C45.4834 8.13703 45.9797 7.7734 46.4084 7.7734C46.9499 7.7734 47.1304 7.97794 47.2657 8.79612C47.4237 9.72794 47.4688 11.7734 47.4914 12.6598C47.5365 14.3188 47.559 17.1143 47.5365 18.8188C47.4914 21.5688 47.4914 24.887 47.559 28.1598C47.559 28.3643 47.8298 28.4325 47.9426 28.2734C50.1762 25.1825 52.9964 20.3188 54.9593 16.1143C56.0422 13.8643 56.8544 12.2734 57.7569 9.90976C58.1179 8.97794 58.4337 8.09158 58.7947 7.45521C59.1332 6.84158 59.6069 6.59158 60.3966 7.36431C60.7576 7.72794 61.0509 8.59158 61.1411 9.25067C61.2765 10.3188 61.096 11.3416 60.6673 12.3416ZM28.0207 19.2961C28.2689 17.6825 29.4872 15.5688 30.886 13.9098C31.5854 13.0916 33.3452 11.5688 34.2251 10.9552C34.8794 10.5007 35.4886 10.1143 36.0977 10.1143C36.4813 10.1143 36.8648 10.2507 37.0453 10.6598C37.2258 11.0461 37.0228 11.5234 36.9325 11.8188C36.3234 13.5916 35.1502 14.8188 33.7965 16.2734C32.3074 17.8643 29.7128 19.3416 28.5847 19.7507C28.2012 19.9325 27.953 19.7961 28.0207 19.2961ZM30.2317 35.3643C32.2171 34.5007 34.3605 32.5916 36.0301 30.637C37.5191 28.9098 39.3241 26.6598 40.5198 24.9552C40.9485 24.3416 41.8058 22.8416 41.9638 22.4098C42.1217 21.9779 42.3473 21.2961 41.9186 21.0007C41.5802 20.7507 41.1064 21.2279 40.8131 21.5688C39.3466 23.3643 36.0977 26.637 34.4733 28.137C33.1873 29.3188 32.0367 30.3188 30.9311 30.9552C30.0287 31.5007 29.2165 31.6143 28.5847 31.3643C27.8628 31.0688 27.5695 30.2507 27.4115 29.2961C27.1633 27.7507 27.3438 24.2734 27.4566 23.6143C27.5695 22.9552 27.8853 22.5688 28.4494 22.4325C29.1262 22.2734 30.0738 21.9098 30.6378 21.6143C31.811 20.9779 32.8714 20.3188 34.1574 19.2507C36.5039 17.3188 38.2411 15.3643 39.4369 11.8643C39.7753 10.8643 39.9783 9.29612 39.3015 8.61431C38.9856 8.29612 38.557 8.0234 38.196 7.8643C37.2484 7.45521 35.8044 7.70521 34.9245 8.06885C32.4202 9.1143 30.5927 10.5007 28.9908 12.1143C28.0884 13.0234 27.0505 14.5234 26.2834 15.887C25.4261 17.4098 24.6139 19.637 24.1852 20.3188C23.8017 20.9325 23.3956 21.1598 22.5608 21.2507C21.6358 21.3643 21.5906 21.3188 20.801 21.4098C20.643 21.4325 20.5077 21.4552 20.3723 21.5461C20.079 21.7734 20.1918 22.2961 20.4625 22.6825C20.6882 23.0234 21.0717 23.1143 21.4778 23.2052C21.9516 23.3188 22.5382 23.3416 23.0346 23.3416C23.6663 23.3416 23.6889 23.4779 23.6212 24.0916C23.4407 25.6143 23.2151 27.887 23.3053 29.5688C23.3956 31.137 23.734 32.7052 24.3431 33.637C24.7944 34.3416 25.6292 35.137 26.4865 35.4325C27.8853 35.9097 29.1036 35.8416 30.2317 35.3643ZM116.552 40.0007V40.7734H115.898L113.89 43.8416V44.9097L114.206 45.2279H115.402V46.0007H111.476V45.2279H112.672L112.988 44.9097V43.8416L110.98 40.7734H110.325V40.0007H113.055V40.7734H112.469L112.153 41.0461L113.462 43.1825L114.77 41.0461L114.477 40.7734H113.868V40.0007H116.552ZM103.219 46.0007V45.2279H103.692L104.008 44.9097V41.1143L103.692 40.7961H103.219V40.0007H105.971C107.55 40.0007 108.34 40.5916 108.34 41.6825C108.34 42.637 107.776 43.1825 106.625 43.3416L108.092 45.1825H109.017V45.9779H107.031V45.2279L105.633 43.387H104.843V44.9097L105.159 45.2279H105.858V46.0007H103.219ZM104.82 42.6143H105.948C107.031 42.6143 107.46 42.2734 107.46 41.6825C107.46 41.0916 107.031 40.7507 105.948 40.7507H104.82V42.6143ZM100.556 40.0007V42.2279H99.7666V41.0916L99.4507 40.7734H97.1269V42.5916H97.9842L98.3001 42.2734V41.637H98.977V44.2734H98.3001V43.637L97.9842 43.3188H97.1269V45.2507H99.4959L99.8117 44.9325V43.8416H100.601V46.0234H95.164V45.2279H95.9763L96.2921 44.9097V41.1143L95.9763 40.7961H95.164V40.0007H100.556ZM92.8176 40.0007V40.7734H92.2536L90.5615 42.5007L92.5243 45.2279H93.0884V46.0007H90.5389V45.2279H91.0353L91.3286 44.9552L89.9523 43.0916L88.8468 44.2052V44.887L89.1627 45.2052H89.6816V45.9779H87.2224V45.2279H87.7413L88.0571 44.9097V41.1143L87.7413 40.7961H87.2224V40.0007H89.6816V40.7734H89.1627L88.8468 41.0916V43.2734L91.0353 41.0461L90.7194 40.7734H90.4035V40.0007H92.8176ZM81.8301 40.0007L79.8673 45.2279H79.213V46.0007H81.6948V45.2279H81.1082L80.7923 44.9552L81.1307 43.9779H83.3869L83.7479 44.9552L83.4771 45.2279H82.8905V46.0007H85.44V45.2279H84.7857L82.8228 40.0007H81.8301ZM82.2814 40.8643L83.1387 43.2279H81.4466L82.2814 40.8643ZM77.769 44.2279C77.769 45.387 76.9568 45.9779 75.3775 45.9779H72.2414V45.2279H73.0537L73.3695 44.9097V41.1143L73.0537 40.7961H72.2414V40.0007H75.2647C76.7312 40.0007 77.4983 40.5688 77.4983 41.637C77.4983 42.2052 77.1599 42.6825 76.483 42.8416C77.1599 42.887 77.769 43.5688 77.769 44.2279ZM76.641 41.637C76.641 41.0916 76.2123 40.7734 75.2421 40.7734H74.2043V42.5234H75.2421C76.2123 42.5234 76.641 42.1825 76.641 41.637ZM74.2043 45.2279H75.3549C76.4379 45.2279 76.8891 44.8643 76.8891 44.2507C76.8891 43.637 76.4153 43.2734 75.3549 43.2734H74.2043V45.2279Z\"\r\n    ></path>\r\n  </svg>\r\n    </div>\r\n    <div style=\"max-width:100%;background-color:#f1f1f1;padding:20px 16px;font-weight:bold;font-size:20px;color:rgb(22,24,35)\">\r\n      Verification Code\r\n    </div>\r\n    <div style=\"max-width:100%;background-color:#f8f8f8;padding:24px 16px;font-size:17px;color:rgba(22,24,35,0.75);line-height:20px\">\r\n      <p style=\"margin-bottom:20px\">To verify your account, enter this code in <span class=\"il\">Levain Bakery</span>:</p>\r\n      <p style=\"margin-bottom:20px;color:rgb(22,24,35);font-weight:bold\">" + code.ToString() + "</p>\r\n      <p style=\"margin-bottom:20px\">Verification codes expire after 24 hours.</p>\r\n      \r\n      \r\n    </div>\r\n\r\n\r\n\r\n  </div>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<BaseResponse<string>> ChangePassword(Guid id, string currentPass, string newPass)
        {
            BaseResponse<string> response = new();

            var user = await _userService.FindByIdAsync(id.ToString());
            var result = await _userService.ChangePasswordAsync(user, currentPass, newPass);

            if (result.Succeeded)
            {
                response.Message = "Change password succesfully";
                response.Code = "200";
            }
            else
            {
                response.Message = "Change password failed";
                response.Code = "202";
            }
            return response;

        }

        public async Task<bool> ForgotPassword(string email, string newPass)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

            if (user == null)
            {
                return false;
            }



            var token = await _userService.GeneratePasswordResetTokenAsync(user);
            var result = await _userService.ResetPasswordAsync(user, token, newPass);

            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<UserDTO> UpdateProfile(UpdateUserRequest request, Guid userID)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(userID));

            if (user == null)
            {
                return null;
            }
            if (request.Avatar != null)
            {

                user.Avatar = request.Avatar;
            }
            if (request.Address != null)
            {

                user.Address = request.Address;
            }
            if (request.DOB != null)
            {

                user.DOB = request.DOB;

            }
            if (request.Address != null)
            {

                user.Address = request.Address;
            }
            if (request.Gender != null)
            {

                user.Gender = request.Gender;
            }

            if (request.FirstName != null)
            {

                user.FirstName = request.FirstName;
            }
            if (request.LastName != null)
            {

                user.LastName = request.LastName;
            }
            //await _userService.UpdateAsync(user);
            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync();
            var dto = MapToDTO(user);


            if (result > 0)
            {
                return dto;
            }
            return null;
        }

        public async Task<ListUserResponse> GetUserList(PaginationFilter filter)
        {
            ListUserResponse response = new();
            PaginationDTO paginationDto = new();

            var orderBy = filter._order.ToString();

            orderBy = orderBy switch
            {
                "1" => "descending",
                "-1" => "ascending",
                _ => orderBy
            };

            dynamic usersInUserRole = null;

            if (filter._all)
            {
                var query = from user in _context.Users
                            join userRole in _context.UserRoles
                                on user.Id equals userRole.UserId
                            join role in _context.Roles
                                on userRole.RoleId equals role.Id
                            where role.Name.Equals("User")
                            select user;
                usersInUserRole = await query.OrderBy(filter._by + " " + orderBy).ToListAsync();

            }
            else
            {
                usersInUserRole = await (from user in _context.Users
                                         join userRole in _context.UserRoles
                                             on user.Id equals userRole.UserId
                                         join role in _context.Roles
                                             on userRole.RoleId equals role.Id
                                         where role.Name.Equals("User")
                                         select user
                   )
                   .OrderBy(filter._by + " " + orderBy)
                   .Skip((filter.PageNumber - 1) * filter.PageSize)
                   .Take(filter.PageSize)
                   .ToListAsync();

            }



            List<UserDTO> userDtoList = new();


            var totalRecords = (from user in _context.Users
                                join userRole in _context.UserRoles
                                    on user.Id equals userRole.UserId
                                join role in _context.Roles
                                    on userRole.RoleId equals role.Id
                                where role.Name.Equals("User")
                                select user
                ).Count();

            if (usersInUserRole == null)
            {
                response.Data = null;
                response.Code = "202";
                response.Message = "There aren't any users in DB";
            }
            else
            {
                foreach (var item in usersInUserRole)
                {
                    userDtoList.Add(MapToDTO(item));
                }

                response.Data = userDtoList;
                response.Message = "SUCCESS";
                response.Code = "200";
            }

            var totalPages = ((double)totalRecords / (double)filter.PageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            paginationDto.CurrentPage = filter.PageNumber;
            paginationDto.PageSize = filter.PageSize;
            paginationDto.TotalPages = roundedTotalPages;
            paginationDto.TotalRecords = totalRecords;

            response.Pagination = paginationDto;


            return response;
        }

        public async Task<ProfileResponseDTO> GetProfile(RefreshToken refreshToken)
        {
            ProfileResponseDTO response = new();
            var tokenHandler = new JwtSecurityTokenHandler();


            var principal = GetPrincipalFromToken(refreshToken.refreshToken);

            if (principal == null)
            {
                response.Code = "501";
                response.Message = "Invalid Token";
                return response;
            }
            if (principal.Identity == null)
            {
                response.Code = "201";
                response.Message = "Please update all information in your profile";
                return response;
            }
            var us = _context.Users.Where(x => x.Token.Equals(refreshToken.refreshToken)).FirstOrDefault();
            if (us == null || us.Token != refreshToken.refreshToken || us.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                response.Code = "500";
                response.Message = "Expired Refresh Token in getProfile";
                return response;
            }
            ProfileDTO profile = new ProfileDTO()
            {
                Id = us.Id,
                UserName = us.UserName,
                Email = us.Email,
                FirstName = us.FirstName,
                LastName = us.LastName,
                Phone = us.PhoneNumber,
                DOB = us.DOB,
                Gender = us.Gender,
                Avartar = us.Avatar

            };

            var role = await _userService.GetRolesAsync(us);
            response.Data = profile;
            response.Role = role[0];
            response.Code = "200";
            response.Message = "msg";
            return response;
        }
    }
}
