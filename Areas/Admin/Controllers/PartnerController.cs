using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGOWebApp.Data;
using NGOWebApp.Models;
using NGOWebApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace NGOWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PartnerController : Controller
    {
        private readonly DatabaseContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PartnerController(DatabaseContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Partner> objList = _db.GetPartners.Include(u=>u.GetDonateCategory).Where(i=>i.Status == 1);
            return View(objList);
        }

        //Get-UpSert

        public IActionResult Upsert(int? Id)
        {
            PartnerVM partnerVM = new PartnerVM()
            {
                Partner = new Partner(),
                CategorySelectList = _db.GetDonateCategories.Where(u=>u.Status == 1).Select(i=> new SelectListItem { 
                
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if(Id == null)
            {
                return View(partnerVM);
            } else
            {
                partnerVM.Partner = _db.GetPartners.Find(Id);
                if(partnerVM.Partner == null)
                {
                    return NotFound();
                }

                return View(partnerVM);
            }
        }

        //Post-Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PartnerVM partnerVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (partnerVM.Partner.Id == 0)
                {
                    //creating
                    string upload = webRootPath + linkImage.imgPathPartner;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    //copyfile to NewLocation
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    partnerVM.Partner.Logo = fileName + extension;
                    partnerVM.Partner.Status = 1;

                    _db.GetPartners.Add(partnerVM.Partner);

                } else
                {
                    //update

                    var objFormDb = _db.GetPartners.AsNoTracking().FirstOrDefault(u => u.Id == partnerVM.Partner.Id);

                    if(files.Count > 0)
                    {
                        string upload = webRootPath + linkImage.imgPathPartner;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //oldfile

                        var oldFile = Path.Combine(upload, objFormDb.Logo);

                        if(System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        //copyfile to NewLocation
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        partnerVM.Partner.Logo = fileName + extension;


                    } else
                    {
                        partnerVM.Partner.Logo = objFormDb.Logo;
                    }
                    partnerVM.Partner.Status = 1;
                    _db.GetPartners.Update(partnerVM.Partner);

                }

                _db.SaveChanges();
                TempData[linkImage.Success] = "Action Complete Successfully";
                return RedirectToAction("Index");
            }

            partnerVM.CategorySelectList = _db.GetDonateCategories.Where(u => u.Status == 1).Select(i => new SelectListItem
            {

                Text = i.Name,
                Value = i.Id.ToString()
            });
          

            return View(partnerVM);

        }


        //GET-Delete
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Partner partner = _db.GetPartners.Include(u => u.GetDonateCategory).FirstOrDefault(u => u.Id == Id);
            // product.Category = _db.Category.Find(product.CategoryId);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        //Post-Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]//seurity
        public IActionResult DeletePost(int? Id)
        {

            var obj = _db.GetPartners.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.Status = 2;
            _db.GetPartners.Update(obj);
            _db.SaveChanges();
            TempData[linkImage.Success] = "Delete Partner Complete Successfully";
            return RedirectToAction("Index");
        }

        //GET-Details
        public IActionResult Detail(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Partner partner = _db.GetPartners.Include(u => u.GetDonateCategory).FirstOrDefault(u => u.Id == Id);
            // product.Category = _db.Category.Find(product.CategoryId);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }



    }
}
