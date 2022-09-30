using BakeryRecipe.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Report
{
    public   class CreateReportRequest
    {
        public int PostID { get; set; }

        public Guid UserID { get; set; }

        public ReportProblem ReportProblem { get; set; }    
    }
}
