using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Interactive
{
    public class InteractiveStatusRequest
    {
        public bool isInteractive { get; set; }

        public bool? isLike { get; set; }

        public bool? isDislike { get; set; }

        public bool? isSave { get; set; }
    }
}
