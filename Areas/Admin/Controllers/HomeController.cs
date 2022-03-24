using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NGOWebApp.Data;
using NGOWebApp.Models;
﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private DatabaseContext context;
        public HomeController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {

            DateTime today = DateTime.Today;
            var Total = from d in context.GetDonates where d.CreatedAt.Month == today.Month && d.CreatedAt.Year == today.Year select d;
            var unSuccess = from d in context.GetDonates where d.CreatedAt.Month == today.Month && d.CreatedAt.Year == today.Year &&d.Status==1 select d;
            var Success = from d in context.GetDonates where d.CreatedAt.Month == today.Month && d.CreatedAt.Year == today.Year &&d.Status==2 select d;
            ViewBag.TotalDonate= Total.Select(x => x.Amount).Sum();
            ViewBag.unSuccess = unSuccess.Select(x => x.Amount).Sum();
            ViewBag.Success = Success.Select(x => x.Amount).Sum();

            double [] totalDay = new double[7];
            for (int i = 0; i < totalDay.Length; i++)
            {
                totalDay[i] = context.GetDonates.Where(x => x.CreatedAt.Date == today.AddDays(-6+i).Date && x.CreatedAt.Month == today.AddDays(-6 + i).Month && x.CreatedAt.Year == today.AddDays(-6 + i).Year).Select(x => x.Amount).Sum();
            }
            ViewBag.totalDays = JsonConvert.SerializeObject(totalDay);
            //if(HttpContext.Session.GetString("FullName") == null || HttpContext.Session.GetInt32("Role") != 1)
            //{
            //    return RedirectToAction("Index", "Home", new { area = "User" });
            //}

            return View();
        }
        public JsonResult GetBarChart()
        {
            var model = (from p in context.GetPartners.OrderByDescending(x=>x.GetDonates.Select(x=>x.Amount).Sum())               
                         select new { PartnerName = p.OrgName,SumDonate=p.GetDonates.Select(x=>x.Amount).Sum() }).Take(6);
            return Json(new { JSONList = model });
        }
        public JsonResult GetPieChart()
        {
            var model = (from c in context.GetDonateCategories
                         select new { CatName = c.Name, SumDonate = c.GetDonates.Select(x => x.Amount).Sum() }).Take(6);
            return Json(new { JSONList = model });
        }
    }
}
