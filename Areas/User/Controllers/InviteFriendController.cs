using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGOWebApp.Models;
using System.IO;
using System.Net.Mail;
using Microsoft.AspNetCore.Server;
using NGOWebApp.Data;
using Microsoft.AspNetCore.Hosting;


namespace NGOWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class InviteFriendController : Controller
    {
        private readonly DatabaseContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public InviteFriendController(DatabaseContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Email email)
        {
            if(ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string linkTempleate = webRootPath + linkImage.emailPath;
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Path.Combine(linkTempleate, "email.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{userName}", email.UserName);
                body = body.Replace("{Content}", email.Description);


                //Email & Content 
                MailMessage message = new MailMessage(new MailAddress("ngowebsitedonate@gmail.com", "Invite to join NGO's Website"), new MailAddress(email.Recipient));
                message.Subject = "Invite to join NGO's Website";
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

                TempData[linkImage.Success] = "Send Mail Complete Successfully";

                return RedirectToAction("Index");
            }


            return View(email);

        }
    }
}
