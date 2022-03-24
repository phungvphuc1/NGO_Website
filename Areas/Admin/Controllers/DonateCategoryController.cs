using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGOWebApp.Data;
using NGOWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace NGOWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonateCategoryController : Controller
    {
        private readonly DatabaseContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public DonateCategoryController(DatabaseContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<DonateCategory> objList = _db.GetDonateCategories.Where(u=>u.Status == 1);
            return View(objList);
        }

        //get-Upsert
        
        public IActionResult Upsert(int? Id)
        {
            DonateCategory DonateCategory = new DonateCategory();

            if(Id == null)
            {
                return View(DonateCategory);
            }
            else
            {
                DonateCategory = _db.GetDonateCategories.Find(Id);
                if(DonateCategory ==  null)
                {
                    return NotFound();
                }
                return View(DonateCategory);
            }
        }

        //Post-Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(DonateCategory donateCategory)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                
                if(donateCategory.Id == 0)
                {
                    //creating
                    string upload = webRootPath + linkImage.imgPath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    //coyfile to NewLocation
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    donateCategory.Photo = fileName + extension;
                    donateCategory.Status = 1;

                    _db.GetDonateCategories.Add(donateCategory);

                } else
                {
                    //updatting
                    var objFromDb = _db.GetDonateCategories.AsNoTracking().FirstOrDefault(u=>u.Id == donateCategory.Id);

                    if(files.Count > 0)
                    {

                        string upload = webRootPath + linkImage.imgPath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //oldfile

                        var oldFile = Path.Combine(upload, objFromDb.Photo);

                        if(System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        //coyfile to NewLocation
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        donateCategory.Photo = fileName + extension;
                    } else
                    {
                        donateCategory.Photo = objFromDb.Photo;
                    }
                    donateCategory.Status = 1;
                    _db.GetDonateCategories.Update(donateCategory);
                }

                _db.SaveChanges();
                TempData[linkImage.Success] = "Action Complete  Successfully";
                return RedirectToAction("Index");


            }

            return View(donateCategory);
        }

        //GET-Delete
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
             DonateCategory donateCategory = _db.GetDonateCategories.FirstOrDefault(u => u.Id == Id);
            // product.Category = _db.Category.Find(product.CategoryId);

            if (donateCategory == null)
            {
                return NotFound();
            }

            return View(donateCategory);
        }

        //Post-Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]//seurity
        public IActionResult DeletePost(int? Id)
        {

            var obj = _db.GetDonateCategories.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.Status = 2;
            _db.GetDonateCategories.Update(obj);
            TempData[linkImage.Success] = "Donate Category Delete Successfully";
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-Details
        public IActionResult Detail(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            DonateCategory donateCategory = _db.GetDonateCategories.FirstOrDefault(u => u.Id == Id);
            // product.Category = _db.Category.Find(product.CategoryId);

            if (donateCategory == null)
            {
                return NotFound();
            }

            return View(donateCategory);
        }

    }
}
