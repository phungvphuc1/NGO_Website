using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models.ViewModels
{
    public class AccountVM
    {
      
        public Account Account { get; set; }

        public bool ExistsAccount { get; set; }

        public bool ExistsPass { get; set; }

        public bool CheckPassword { get; set; }
    }
}
