using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models.ViewModels
{
    public class ProgramDonateVM
    {
        public Programs Programs { get; set; }
        public double  SumDonate { get; set; }
        public int DateDiff { get; set; }
        public bool Interested { get; set; }
    }
}
