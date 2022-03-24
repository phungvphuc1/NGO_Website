using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
