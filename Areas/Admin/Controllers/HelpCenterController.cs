using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGOWebApp.Data;
using NGOWebApp.Models.ViewModels;
using NGOWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace NGOWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HelpCenterController : Controller
    {
        private readonly DatabaseContext _db;

        public HelpCenterController(DatabaseContext db)
        {
            
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Query> objList = _db.GetQueries.Include(u => u.GetAccount).Where(u => u.Status == 1  || u.Status == 3);
            return View(objList);
        }

        public IActionResult DetailReplies(int? Id)
        {
            QuestionVM questionVM = new QuestionVM()
            {
                query = new Query(),
                reply = new Reply()
            };

            if (Id == null)
            {
                return View(questionVM);
            }
            else
            {
                questionVM.query = _db.GetQueries.Include(u => u.GetAccount).FirstOrDefault(i => i.Id == Id);
                questionVM.Replies = _db.GetReplies.Include(u => u.GetAccount).Include(i => i.GetQuery).Where(u => u.Status == 1 && u.QueryId == Id);
                questionVM.Queries = _db.GetQueries.Include(u => u.GetAccount).Where(u => u.Status == 1 || u.Status == 3);
                if (questionVM.query == null)
                {
                    return NotFound();
                }
                return View(questionVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetailReplies(QuestionVM questionVM)
        {
            questionVM.query = _db.GetQueries.AsNoTracking().Include(u => u.GetAccount).FirstOrDefault(i => i.Id == questionVM.query.Id);
            questionVM.Replies = _db.GetReplies.AsNoTracking().Include(u => u.GetAccount).Include(i => i.GetQuery).Where(u => u.Status == 1 && u.QueryId == questionVM.query.Id);
            questionVM.Queries = _db.GetQueries.AsNoTracking().Include(u => u.GetAccount).Where(u => u.Status == 1 || u.Status == 3);

            if (questionVM.reply.Id == 0)
            {
                questionVM.query.Status = 3;
                _db.GetQueries.Update(questionVM.query);
                _db.GetReplies.Add(questionVM.reply);
                _db.SaveChanges();
                TempData[linkImage.Success] = "Answer Action Complete Successfully";
                return View(questionVM);

            }



            return View(questionVM);
        }

        //delete answered

        public IActionResult DeleteAnswered(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Reply reply = _db.GetReplies.Include(u=>u.GetAccount).Include(u=>u.GetQuery).FirstOrDefault(u => u.Id == Id);
            // product.Category = _db.Category.Find(product.CategoryId);

            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }
        [HttpPost, ActionName(" DeleteAnswered")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAnsweredPost(int? Id)
        {
            var obj = _db.GetReplies.Find(Id);
            if(obj == null)
            {
                return NotFound();
            } else
            {
                obj.Status = 2;
                _db.GetReplies.Update(obj);
                _db.SaveChanges();
                TempData[linkImage.Success] = "Deleted Action Complete Successfully";
                return RedirectToAction("DetailReplies", new { id = obj.QueryId });
            }
          
        }

        //Index View Common Question
        public IActionResult IndexCommon()
        {
            IEnumerable<Query> objList = _db.GetQueries.Include(u => u.GetAccount).Where(u => u.Status == 4);
            return View(objList);
           
        }


        //Get create and update Common Question

        public IActionResult Upsert(int? Id)
        {
            Query query = new Query();

            if (Id == null)
            {
                return View(query);
            }
            else
            {
                query = _db.GetQueries.Find(Id);
                if (query == null)
                {
                    return NotFound();
                }
                return View(query);
            }
        }

        //Post create and update Common Question


        //Post-Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Query query)
        {
            if (ModelState.IsValid)
            {
               

                if (query.Id == 0)
                {
                    //creating
             

                  
                    query.Status = 4;

                    _db.GetQueries.Add(query);

                }
                else
                {
                    //updatting
                    query.Status = 4;
                    _db.GetQueries.Update(query);
                }

                _db.SaveChanges();
                TempData[linkImage.Success] = "Action Complete Successfully";
                return RedirectToAction("IndexCommon");


            }
            TempData[linkImage.Error] = "Action is  error. Please check again";
            return View(query);
        }


        //Delet common quesion
        //GET-Delete
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Query query = _db.GetQueries.FirstOrDefault(u => u.Id == Id);
            // product.Category = _db.Category.Find(product.CategoryId);

            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        //Post-Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]//seurity
        public IActionResult DeletePost(int? Id)
        {

            var obj = _db.GetQueries.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.Status = 2;
            _db.GetQueries.Update(obj);
            _db.SaveChanges();
            TempData[linkImage.Success] = "Deleted Commom Question Complete Successfully";
            return RedirectToAction("IndexCommon");
        }


        //GET-Details
        public IActionResult Detail(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Query query = _db.GetQueries.FirstOrDefault(u => u.Id == Id);
            // product.Category = _db.Category.Find(product.CategoryId);

            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }
    }
}
