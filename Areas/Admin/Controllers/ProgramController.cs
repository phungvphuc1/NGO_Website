using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NGOWebApp.Data;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.WebPages.Html;

namespace NGOWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProgramController : Controller
    {
        private DatabaseContext context;
        public ProgramController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var model = context.GetPrograms.Include(x=>x.GetPartner).Where(x=>x.DeleteAt==null).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var partnerList = context.GetPartners.Include(x => x.GetPrograms).Where(x => x.GetPrograms.Where(x => x.Status == 1 && x.DeleteAt == null).Count() == 0).ToList();
            ViewBag.partnerList = new SelectList(partnerList, "Id", "OrgName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Programs programs,IFormFile file)
        {
            
            if (ModelState.IsValid)
            {
                var program = context.GetPrograms.SingleOrDefault(x => x.PartnerId == programs.PartnerId && x.Status == 1&&x.DeleteAt==null);
                if (program!=null)
                {
                    ViewBag.Exist = "This partner is calling for a campaign";
                    var partnerList = context.GetPartners.Include(x=>x.GetPrograms).Where(x=>x.GetPrograms.Where(x=>x.Status==1&&x.DeleteAt==null).Count()==0).ToList();
                    ViewBag.partnerList = new SelectList(partnerList, "Id", "OrgName");
                    return View();
                }
                else
                {
                    try
                    {
                        if (file != null)
                        {
                            string path = Path.Combine("wwwroot/images/AlbumProgram/", file.FileName);
                            var stream = new FileStream(path, FileMode.Create);
                            file.CopyToAsync(stream);
                            programs.Photo = "images/AlbumProgram/" + file.FileName;
                        }
                        else
                        {
                            ViewBag.Msg = "File not found!";
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.GetPrograms.Add(programs);
                            context.SaveChanges();
                            //send donations with programId null value to this program                     
                            var donates = context.GetDonates.Where(x => x.PartnerId == programs.PartnerId && x.Status == 1 && x.ProgramId == null);                          
                            if (donates.Any())
                            {
                                foreach (var item in donates)
                                {
                                    item.ProgramId = programs.Id;
                                }
                                context.SaveChanges();
                            }
                            
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return BadRequest();
                        }
                    }
                   
                    
                    return RedirectToAction("Index", "Program");
                }
               
            }
            else
            {
                var partnerList = context.GetPartners.ToList();
                ViewBag.partnerList = new SelectList(partnerList, "Id", "OrgName");
                return View();
            }          
        }
        public IActionResult Edit(int id)
        {
            var partnerList = context.GetPartners.ToList();
            ViewBag.partnerList = new SelectList(partnerList, "Id", "OrgName");           
            return View(context.GetPrograms.Include(x=>x.GetPartner).Where(x=>x.Id==id).SingleOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Programs programs)
        {
            if (ModelState.IsValid)
            {
                var model = context.GetPrograms.Find(programs.Id);
                model.ShortContent = programs.ShortContent;
                model.PartnerId = programs.PartnerId;
                model.ExpectedAmount = programs.ExpectedAmount;
                model.Status = programs.Status;
                model.Content = programs.Content;
                model.UpdatedAt = DateTime.Now;
                context.SaveChanges();
                return RedirectToAction("Index", "Program");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Delete(int id)
        {
            var model = context.GetPrograms.Find(id);
            if (model!=null)
            {
                model.DeleteAt = DateTime.Now;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
           
           
        }
    }
}
