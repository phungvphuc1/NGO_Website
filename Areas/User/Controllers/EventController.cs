using Microsoft.AspNetCore.Http;
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
    public class EventController : Controller
    {
        private readonly DatabaseContext context;
        public EventController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var accountId = HttpContext.Session.GetInt32("Id");
            var model = from p in context.GetPrograms.Include(x => x.GetDonates).Include(x => x.GetPartner).Include(x=>x.GetInteresteds).Where(x=>x.DeleteAt==null).OrderBy(x=>x.Status)
                       
                        select new ProgramDonateVM
                        {
                            Programs = p,
                            SumDonate = p.GetDonates.Select(x => x.Amount).Sum(),
                            DateDiff = (p.Duration - DateTime.Now).Days,
                            Interested=p.GetInteresteds.SingleOrDefault(x=>x.AccountId==accountId)==null?false:true
                        };

            var result = model.ToList();
            return View(result);
        }
        public IActionResult Details(int Id)
        {
            return View(context.GetPrograms.Include(x=>x.GetPartner).Where(x=>x.Id==Id).Single());
        }
    }
}
