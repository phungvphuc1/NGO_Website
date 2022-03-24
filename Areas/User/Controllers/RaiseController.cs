using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGOWebApp.Data;
using NGOWebApp.Models;
using NGOWebApp.Models.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace NGOWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class RaiseController : Controller
    {
        private readonly DatabaseContext _db;

        public RaiseController(DatabaseContext db)
        {
            _db = db;

        }
        public IActionResult Index(int? page)
        {
            HttpContext.Session.Remove("CheckNav");

            var pageNumber = page ?? 1;
            int pageSize = 5;

            QuestionVM questionVM = new QuestionVM()
            {
                Queries = _db.GetQueries.Include(u => u.GetAccount).Where(u=>u.Status != 2),
                Replies = _db.GetReplies.Include(u => u.GetAccount).Include(i => i.GetQuery).Where(u=>u.Status == 1),
                OnePageQueries = _db.GetQueries.Include(u=>u.GetAccount).Where(u => u.Status == 1 || u.Status == 3).ToPagedList(pageNumber, pageSize)

            };
       
            return View(questionVM);
        }

        public IActionResult IndexAcccout(int? page)
        {
            HttpContext.Session.Remove("CheckNav");
            //add session
            HttpContext.Session.SetInt32("CheckNav", 2);
            var pageNumber = page ?? 1;
            int pageSize = 5;
            QuestionVM questionVM = new QuestionVM()
            {
                Queries = _db.GetQueries.Include(u => u.GetAccount).Where(u => u.Status != 2),
                Replies = _db.GetReplies.Include(u => u.GetAccount).Include(i => i.GetQuery).Where(u => u.Status == 1),
                 OnePageQueries = _db.GetQueries.Include(u => u.GetAccount).Where(u => u.Status != 2 && u.AccountId == HttpContext.Session.GetInt32("AccountId")).ToPagedList(pageNumber, pageSize)

            };

            return View(questionVM);
        }

        public IActionResult QuestionCreate()
        {
            QuestionVM questionVM = new QuestionVM()
            {
                Queries = _db.GetQueries.Include(u => u.GetAccount).Where(u => u.Status != 2),
                Replies = _db.GetReplies.Include(u => u.GetAccount).Include(i => i.GetQuery).Where(u => u.Status == 1),
                query = new Query()

            };
            return View(questionVM);
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult QuestionCreate(QuestionVM questionVM)
        {
            if(ModelState.IsValid)
            {
                _db.GetQueries.Add(questionVM.query);
                _db.SaveChanges();
                TempData[linkImage.Success] = "New Issue Create Complete Succesfullly";
                if (HttpContext.Session.GetInt32("CheckNav") != null)
                {
                    return RedirectToAction("IndexAcccout");
                }
                return RedirectToAction("Index");
            }

            TempData[linkImage.Error] = "New Issue cannot create. Please check again";
            return View(questionVM.query);
        }
        
        [HttpGet]
        public IActionResult QuestionView(int? Id)
        {
            QuestionVM questionVM = new QuestionVM()
            {
                query = new Query(),
                reply = new Reply()
            };

            if(Id == null)
            {
                return View(questionVM);
            }else
            {
                questionVM.query = _db.GetQueries.Include(u=>u.GetAccount).FirstOrDefault(i=>i.Id == Id);
                questionVM.Replies = _db.GetReplies.Include(u => u.GetAccount).Include(i => i.GetQuery).Where(u => u.Status == 1 && u.QueryId == Id);
                questionVM.Queries = _db.GetQueries.Include(u => u.GetAccount).Where(u => u.Status != 2);
                if (questionVM.query == null)
                {
                    return NotFound();
                }
                return View(questionVM);
            }


            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuestionView(QuestionVM questionVM)
        {
            questionVM.query = _db.GetQueries.AsNoTracking().Include(u => u.GetAccount).FirstOrDefault(i => i.Id == questionVM.query.Id);
            questionVM.Replies = _db.GetReplies.AsNoTracking().Include(u => u.GetAccount).Include(i => i.GetQuery).Where(u => u.Status == 1 && u.QueryId == questionVM.query.Id);
            questionVM.Queries = _db.GetQueries.AsNoTracking().Include(u => u.GetAccount).Where(u => u.Status != 2 );
          
                if (questionVM.reply.Id == 0)
            {
                questionVM.query.Status = 1;
                _db.GetQueries.Update(questionVM.query);
                _db.GetReplies.Add(questionVM.reply);
                _db.SaveChanges();
                TempData[linkImage.Success] = "Post answerde is completed successfully";
                return View(questionVM);

            }



            return View(questionVM);
        }

        // view common question 

        public IActionResult CommonView(int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 5;
            QuestionVM questionVM = new QuestionVM()
            {
                Queries = _db.GetQueries.Include(u => u.GetAccount).Where(u => u.Status != 2),
                Replies = _db.GetReplies.Include(u => u.GetAccount).Include(i => i.GetQuery).Where(u => u.Status == 1),
                 OnePageQueries = _db.GetQueries.Include(u => u.GetAccount).Where(u => u.Status == 4).ToPagedList(pageNumber, pageSize)
            };

            return View(questionVM);
        }
    }
}
