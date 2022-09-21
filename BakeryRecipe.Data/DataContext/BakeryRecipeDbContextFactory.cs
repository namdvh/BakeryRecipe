using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Data.DataContext
{
    public class BakeryRecipeDbContextFactory : IDesignTimeDbContextFactory<BakeryDBContext>
    {
        public BakeryDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
            var optionsBuilder = new DbContextOptionsBuilder<BakeryDBContext>();
            optionsBuilder.UseSqlServer("BakeryRecipeDb");
            return new BakeryDBContext(optionsBuilder.Options);
        }
    }
}
