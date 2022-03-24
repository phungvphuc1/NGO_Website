using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGOWebApp.Data;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace NGOWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MAccountController : Controller
    {
        private readonly DatabaseContext context;
        public MAccountController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int pageindex = 1)
        {
            var acc = context.GetAccounts.Where(a => a.Status == 1);        
            var model = PagingList.Create(acc,5, pageindex);
            return View(model);
        }
      
        public IActionResult DeleteAccount(int id)
        {
            var Delacc =context.GetAccounts.SingleOrDefault(m => m.Id.Equals(id));
            if (Delacc!=null)
            {
                Delacc.Status = 2;
                context.SaveChanges();               
            }           
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult UpdateAccount(int id)
        {
            var acc = context.GetAccounts.SingleOrDefault(a => a.Id.Equals(id));
            return View(acc);
        }
        [HttpPost]
        public IActionResult UpdateAccount(NGOWebApp.Models.Account acc)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    var Upacc = context.GetAccounts.SingleOrDefault(m => m.Id.Equals(acc.Id));
                    if (Upacc != null)
                    {
                        Upacc.FullName = acc.FullName;
                        Upacc.Email = acc.Email;
                        Upacc.Password = acc.Password;
                        Upacc.Phone = acc.Phone;
                        Upacc.Address = acc.Address;
                        Upacc.Birthday = acc.Birthday;
                        Upacc.Avatar = acc.Avatar;
                        Upacc.RoleId = acc.RoleId;
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                
                
            } catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        public IActionResult ResetPass(int id)
        {
            var acc = context.GetAccounts.SingleOrDefault(m => m.Id.Equals(id));
            if (acc != null)
            {
                acc.Password = "12345678";
                context.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAccount(NGOWebApp.Models.Account acc)
        {
            try 
            {
                var newAcc = context.GetAccounts.SingleOrDefault(a => a.Email.Equals(acc.Email));
                if (newAcc==null)
                {
                    if (ModelState.IsValid)
                    {
                        acc.RoleId = 1;
                        acc.Password = GetMD5.CheckMD5(acc.Password);
                        context.GetAccounts.Add(acc);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.msg = "Account has existed";
                }
            } catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();

        }

        public IActionResult ContactUs()
        {    
            var model= context.GetContactUs.ToList();
            return View(model);
        }
    }
}
