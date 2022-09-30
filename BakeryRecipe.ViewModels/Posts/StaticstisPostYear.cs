using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Posts
{
    public class StaticstisPostYear
    {
        public StaticstisPostYear()
        {
        }

        public StaticstisPostYear(int postCount, string year)
        {
            PostCount = postCount;
            Year = year;
        }

        public int PostCount { get; set; }
        public string Year { get; set; }
    }
}
