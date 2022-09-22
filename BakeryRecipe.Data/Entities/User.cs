using BakeryRecipe.Data.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.Entities
{
    public class User: IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? Avatar { get; set; }
        public Status Status { get; set; }
        public string? Token { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public Provider? Provider { get; set; }
        public string? Code { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Interactive>? Interactives { get; set; }
        public List<Repost>? Reposts { get; set; }
        public List<Report>? Reports { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Product>? Products { get; set; }
    }
}
