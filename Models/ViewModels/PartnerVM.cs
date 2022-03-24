using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace NGOWebApp.Models.ViewModels
{
    public class PartnerVM
    {
        public Partner Partner { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}
