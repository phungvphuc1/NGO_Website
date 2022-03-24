using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models.ViewModels
{
    public class DonateVM
    {
        public  Donate Donate { get; set; }
        public Partner Partner { get; set; }    
        public double TotalMoney { get; set; }
        public double CurrentMoney { get; set; }
    }
}
