using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGOWebApp.Data;
using NGOWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class CauseController : Controller
    {
        private readonly DatabaseContext context;
        public CauseController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            ViewBag.CauseList = context.GetDonateCategories.Include(x=>x.GetPartners).Where(x=>x.Status==1).ToList();
            var model = from p in context.GetPrograms.Include(x=>x.GetDonates).Include(x=>x.GetPartner) .Where(x=>x.Status==1&&x.DeleteAt==null)
                
                        select new ProgramDonateVM {
                            Programs = p,
                            SumDonate = p.GetDonates.Select(x=>x.Amount).Sum(),
                            DateDiff=(p.Duration-DateTime.Now).Days
                        };
           
            var result = model.ToList();
            return View(result);
        }
    }
}
