using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NGOWebApp.Data;
using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models.ViewModels;

namespace NGOWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly DatabaseContext context;
        public HomeController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var model = from p in context.GetPrograms.Include(x => x.GetDonates).Include(x => x.GetPartner).Where(x => x.Status == 1&&x.DeleteAt==null)

                        select new ProgramDonateVM
                        {
                            Programs = p,
                            SumDonate = p.GetDonates.Select(x => x.Amount).Sum(),
                            DateDiff = (p.Duration - DateTime.Now).Days
                        };

            var result = model.ToList();
            return View(result);
        }
    }
}
