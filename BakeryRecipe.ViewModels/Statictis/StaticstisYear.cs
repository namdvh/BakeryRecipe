using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Statictis
{
    public class StaticstisYear
    {
        public StaticstisYear()
        {
        }

        public StaticstisYear(int postCount, string year)
        {
            Count = postCount;
            Year = year;
        }

        public int Count { get; set; }
        public string Year { get; set; }
    }
}
