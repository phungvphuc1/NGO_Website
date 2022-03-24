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
    public class PartnerController : Controller
    {
        private readonly DatabaseContext context;
        public PartnerController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var model = from e in context.GetPartners.Include(x => x.GetPrograms).Where(x=>x.Status==1)
                        select new PartnerProgramVM { GetPartner=e,SumProgram=e.GetPrograms.Count()};
            return View(model);
        }
    }
}
