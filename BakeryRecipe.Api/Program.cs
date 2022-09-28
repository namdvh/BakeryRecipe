using BakeryRecipe.Application.System.Categories;
using BakeryRecipe.Application.System.Posts;
using BakeryRecipe.Application.System.Products;
using BakeryRecipe.Application.Comments;
using BakeryRecipe.Application.System.Users;
using BakeryRecipe.Constants;
using BakeryRecipe.Data.DataContext;
using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:4000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BakeryRecepi.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
});
string issuer = builder.Configuration.GetValue<string>("Tokens:Issuer");
string signingKey = builder.Configuration.GetValue<string>("Tokens:Key");
byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = issuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = System.TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = async (context) =>
            {
                Console.WriteLine("Printing in the delegate OnChallenge");

                context.HandleResponse();

                if (context.AuthenticateFailure != null)
                {
                    //DentistResponse response = new();
                    //response.Message = "Token Validation Has Failed. Request Access Denied";
                    //response.Code = "900";
                    if (!context.Response.HasStarted)
                    {
                        string result;
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        result = JsonConvert.SerializeObject(new { code = "900", message = "Token Validation Has Failed. Request Access Denied" });
                        context.Response.ContentType = "application/json";
                        await context.HttpContext.Response.WriteAsync(result);
                    }

                }
            },
            OnAuthenticationFailed = async (context) =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    //context.Response.StatusCode = 200;
                    //DentistResponse response = new();
                    string result;
                    //response.Message = "Token has expired";
                    //response.Code = "901";
                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        result = JsonConvert.SerializeObject(new { code = "901", message = "Token has expired" });
                        context.Response.ContentType = "application/json";
                        await context.HttpContext.Response.WriteAsync(result);
                    }
                }
            }
        };

    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//DBContext
builder.Services.AddDbContext<BakeryDBContext>(options => options.
           UseSqlServer(builder.Configuration.GetConnectionString(Constants.SystemsConstant.MainConnectionString)));
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<BakeryDBContext>().AddDefaultTokenProviders();

//Delcare DI
builder.Services.AddScoped<UserManager<User>, UserManager<User>>();
builder.Services.AddScoped<SignInManager<User>, SignInManager<User>>();
builder.Services.AddScoped<RoleManager<Role>, RoleManager<Role>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IInteractiveService, InteractiveService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IValidator<RegisterRequestDTO>, RegisterRequestValidatorDTO>();
//builder.Services.AddCors(o =>
//{
//    o.AddPolicy("MyPolicy", builder =>
//builder.WithOrigins("https://localhost:4000")
//           .AllowAnyHeader()
//           .AllowCredentials()
//           .AllowAnyMethod());
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BakeryRecepi.Api v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});
app.MapControllers();

app.Run();
