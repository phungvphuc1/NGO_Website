using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NGOWebApp.Models;
using NGOWebApp.Data;
using NGOWebApp.Models.ViewModels;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Mail;
using Microsoft.AspNetCore.Server;


namespace NGOWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private readonly  DatabaseContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountController(DatabaseContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Email, string Password)
        {
            if(ModelState.IsValid)
            {
                var objAccount  = _db.GetAccounts.Where(u => u.Email == Email && u.Status == 1).FirstOrDefault();
                if(objAccount != null)
                {
                    var verified = GetMD5.CheckMD5(Password);
                    if(objAccount.Password.Equals(verified))
                    {
                        //add session
                        HttpContext.Session.Clear();
                        HttpContext.Session.SetString("FullName", objAccount.FullName);
                        HttpContext.Session.SetInt32("Role", objAccount.RoleId);
                        HttpContext.Session.SetInt32("AccountId", objAccount.Id);
                        HttpContext.Session.SetInt32("Id", objAccount.Id);

                        if (objAccount.RoleId == 1)
                        {
                            TempData[linkImage.Success] = "Login Action Successfully";
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        } else
                        {
                            TempData[linkImage.Success] = "Login Action Successfully";
                            return RedirectToAction("Index", "Home", new { area = "User" });
                        }

                    } else
                    {
                        ViewBag.Error = "Password does not match";
                        return View();
                    }

                } else
                {
                    ViewBag.ExistUser = "Email is not reigister";
                    return View();
                }
            
                

            }

            TempData[linkImage.Error] = "Login Action is error. Please check again";
         
            return View();
        }
   
        //Log out
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        //Register
        [HttpGet]
        public IActionResult Register()
        {
            AccountVM accountVM = new AccountVM()
            {
                Account = new Account(),
                ExistsAccount = false

            };
            return View(accountVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AccountVM accountVM)
        {
            if(ModelState.IsValid)
            {
                var accountCheck = _db.GetAccounts.Where(u => u.Email == accountVM.Account.Email).FirstOrDefault();
                if(accountCheck == null || (accountCheck != null && accountCheck.Status == 2))
                {
                    accountVM.Account.Password = GetMD5.CheckMD5(accountVM.Account.Password);
                    _db.GetAccounts.Add(accountVM.Account);
                    _db.SaveChanges();
                    TempData[linkImage.Success] = "Register Complete Succesfullly. Please login";
                    return RedirectToAction("Login");
                }
                else
                {
                    accountVM.ExistsAccount = true;
                    return View(accountVM);
                }
            }

            return View(accountVM);
        }

        //Change Password
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(string newPass,string oldPass)
        {
            
            if (String.IsNullOrEmpty(oldPass))
            {
                ViewBag.oldPass = "Please input current Pass!";
            }
            else if(String.IsNullOrEmpty(newPass))
            {
                ViewBag.newPass = "Please input New Password!";
            }
            else
            {
                if (newPass.Equals(oldPass))
                {
                    ViewBag.newPass = "New Password must different!";
                    return View();
                }
                else
                {
                    var id = HttpContext.Session.GetInt32("Id");
                    var account = _db.GetAccounts.Find(id);
                    if (account.Password.Equals(GetMD5.CheckMD5(oldPass)))
                    {
                        account.Password = GetMD5.CheckMD5(newPass);
                        _db.SaveChanges();
                        ViewBag.Success = "Reset Password success!!";
                        return View();
                    }
                    else
                    {
                        ViewBag.oldPass = "Password incorrect!";
                    }
                }
            }
            return View();
        }

        //Forgot password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, ActionName("ForgotPassword")]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPasswordPost(Account account)
        {
          
                var objAccount = _db.GetAccounts.AsNoTracking().FirstOrDefault(u=>u.Email == account.Email);
                if (objAccount == null)
                {
                    ViewBag.Error = "Email does not exit.Please register";
                    return View(account);
                } else
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string linkTempleate = webRootPath + linkImage.emailPath;
                string resetCode = Guid.NewGuid().ToString();
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Path.Combine(linkTempleate, "emailRessetPassword.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{RessetPassword}", resetCode);


                //Email & Content 
                MailMessage message = new MailMessage(new MailAddress("ngowebsitedonate@gmail.com", "Resset Your Password"), new MailAddress(objAccount.Email));
                message.Subject = "Resset Your Password";
                message.Body = body;
                message.IsBodyHtml = true;


                //Server Details
                SmtpClient smtp = new SmtpClient();
                //Outlook ports - 465 (SSL) or 587 (TLS)
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //Credentials
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                credentials.UserName = "ngowebsitedonate@gmail.com";
                credentials.Password = "team4pro";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credentials;

                smtp.Send(message);
                objAccount.Password = resetCode;
                _db.GetAccounts.Update(objAccount);
                _db.SaveChanges();
                TempData[linkImage.Success] = "Passowrd is sent to your email. Please check your email";

                return RedirectToAction(nameof(ResstPassword));
            }
          

        }

        //Change Password after resset 
        [HttpGet]
        public IActionResult ResstPassword()
        {
            return View();
        }
        [HttpPost, ActionName("ResstPassword")]
        [ValidateAntiForgeryToken]
        public IActionResult ResstPassword(string? ressetPass, Account account)
        {
            if(ressetPass != null)
            {
                var objAccount = _db.GetAccounts.AsNoTracking().FirstOrDefault(u => u.Password == ressetPass);
                if (objAccount == null)
                {
                    ViewBag.Error = "Your code is error. Please input code from your email";
                    return View();
                } else
                {
                    objAccount.Password = GetMD5.CheckMD5(account.Password);
                    _db.GetAccounts.Update(objAccount);
                    _db.SaveChanges();
                    TempData[linkImage.Success] = "Resset password is successfull. Please login to your account";

                    return RedirectToAction(nameof(Login));
                }

            } else
            {
                ViewBag.Error = "Please input your code to resset your password";
                return View();
            }
          
        }



        //User profile
        public IActionResult UserProfile()
        {
            
            if (HttpContext.Session.GetString("FullName") == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var id = HttpContext.Session.GetInt32("Id");
                var account = _db.GetAccounts.Find(id);
                return View(account);
            }
        }
        //Transaction

        [HttpGet]        
        public IActionResult Transaction()
        {
            var id = HttpContext.Session.GetInt32("Id");
            if (id==null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var transac = _db.GetDonates.Include(x => x.GetPrograms).Include(x=>x.GetPartner).Include(x=>x.GetDonateCategory).Where(x=>x.AccountId==id).OrderByDescending(x=>x.CreatedAt).ToList();
                return View(transac);
            }
           
        }
        [HttpGet]
        public IActionResult Activity()
        {
            var id = HttpContext.Session.GetInt32("Id");
            if (id == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var activity = _db.GetInteresteds.Include(x => x.GetPrograms).Include(x=>x.GetAccount).Where(x=>x.AccountId==id).OrderByDescending(x=>x.CreatedAt).ToList();
                return View(activity);
            }

        }

    }
}
