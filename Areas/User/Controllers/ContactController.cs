using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class ContactController : Controller
    {
        private readonly Data.DatabaseContext context;
        public ContactController(Data.DatabaseContext context) {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Models.ContactUs contact)
        {
            context.Add(contact);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
