using BakeryRecipe.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Report
{
    public class ReportDetailDTO
    {
        public int PostID { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }

        public ReportProblem ReportProblem { get; set; }
    }
}
