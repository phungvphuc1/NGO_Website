using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Models.ViewModels
{
    public class DonateCategoriesVM
    {
        public int CategoryId { get; set; }
        public int PartnerId { get; set; }

        public Donate GetDonate { get; set; }
    }
}
