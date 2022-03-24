using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGOWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InterestedController : Controller
    {
        private readonly DatabaseContext context;
        public InterestedController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var model = context.GetAccounts.Include(x => x.GetInteresteds).ThenInclude(x=>x.GetPrograms).ThenInclude(x=>x.GetPartner).Where(x=>x.GetInteresteds.Count()>0);
            var list = model.ToList();
            return View(list);
        }
    }
}
