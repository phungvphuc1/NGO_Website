using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NGOWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NGOWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class DonateController : Controller
    {
        // GET: DonateController
        private readonly DatabaseContext context;
        public DonateController(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<ActionResult> Index()
        {
            var model =await context.GetDonates.Include(d=>d.GetAccount).Include(d=>d.GetPartner).Include(d=>d.GetDonateCategory).OrderByDescending(d=>d.CreatedAt).ToListAsync();
            return View(model);
        }

        public async Task<ActionResult> ViewPartner()
        {
            var model = await context.GetDonates.Include(x=>x.GetPartner).ToListAsync();
            var list = from e in context.GetPartners select e.OrgName.ToString();
            ViewBag.PartnerList = list.ToArray();
            return View(model);
        }

       
    }
}
