using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NGOWebApp.Data;
using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using NGOWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace NGOWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class DonateController : Controller
    {
        private readonly DatabaseContext context;
        public DonateController(DatabaseContext _context)
        {
            this.context = _context;
        }
        public ActionResult Index()
        {
            ViewBag.DonateList = new SelectList(GetCategoriesList(), "Id", "Name");
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public List<DonateCategory> GetCategoriesList()
        {
            return context.GetDonateCategories.Where(x=> x.Status == 1).ToList();
             
        }

        public ActionResult GetPartnerList(int CategoryId)
        {
            List<Partner> partners = context.GetPartners.Where(x => x.CategoryId == CategoryId && x.Status == 1).ToList();
            ViewBag.PartnerList = new SelectList(partners, "Id", "OrgName");
            return PartialView("DisplayPartner");
        }

        [HttpPost]
        public IActionResult Create(DonateCategoriesVM donateCategoriesVM)
        {
            try
            {
                //check campaign of partner : if exist =>donate to this campaign /else set null
                var program= context.GetPrograms.FirstOrDefault(x => x.PartnerId == donateCategoriesVM.PartnerId && x.Status == 1);
                Donate donate = new Donate();
                donate.AccountId = donateCategoriesVM.GetDonate.AccountId;
                donate.CategoryId = donateCategoriesVM.CategoryId;
                donate.PartnerId = donateCategoriesVM.PartnerId;
                donate.Amount = donateCategoriesVM.GetDonate.Amount;
                if (program!=null)
                {
                    donate.ProgramId = program.Id;
                }
                else
                {
                    donate.ProgramId = null;
                }              
                donate.Status = 1;
                context.GetDonates.Add(donate);
                context.SaveChanges();
                return RedirectToAction("Success");
            }
            catch (Exception)
            {
            }
           return RedirectToAction("Index");
        }

        //DonateToProgram
        [HttpGet]
        public ActionResult DonateToProgram(int programId)
        {
            if (HttpContext.Session.GetInt32("AccountId")==null) 
            {
                return RedirectToAction("Login", "Account");
            }
            var model = context.GetPrograms.Include(x => x.GetPartner).ThenInclude(x => x.GetDonateCategory).Where(x => x.Id == programId).FirstOrDefault();
            var info = new Dictionary<string, int>();
            info.Add("Category", model.GetPartner.GetDonateCategory.Id);
            info.Add("PartnerId", model.GetPartner.Id);
            info.Add("ProgramId", model.Id);
            ViewBag.Info = info;
            return View();
        }
        [HttpPost]
        public ActionResult DonateToProgram(Donate donate)
        {
            try
            {
                context.GetDonates.Add(donate);
                context.SaveChanges();

                //close the campaign when the donation is enough
                var model = context.GetPrograms.Include(x => x.GetDonates).Where(x=>x.Id==donate.ProgramId).Where(x => x.GetDonates.Select(x => x.Amount).Sum() >= x.ExpectedAmount).FirstOrDefault();
                if (model!=null)
                {
                    model.Status = 2;
                    context.SaveChanges();
                }
                return RedirectToAction("Success");


            }
            catch (Exception)
            {
                var model = context.GetPrograms.Include(x => x.GetPartner).ThenInclude(x => x.GetDonateCategory).Where(x => x.Id == donate.ProgramId).FirstOrDefault();
                var info = new Dictionary<string, int>();
                info.Add("Category", model.GetPartner.GetDonateCategory.Id);
                info.Add("PartnerId", model.GetPartner.Id);
                info.Add("ProgramId", model.Id);
                ViewBag.Info = info;
                return View();
            }
           
        }

    }
}
