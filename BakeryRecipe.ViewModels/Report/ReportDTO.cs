using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Report
{
    public class ReportDTO
    {
        public string Title { get; set; } = string.Empty;

        public DateTime? Date { get; set; }

        public string? Image { get; set; }

        public int PostID { get; set; }


        public int Count { get; set; }
    }
}
