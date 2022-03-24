using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class ReplySeeders
    {
        public ReplySeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reply>().HasData(
                new Reply {Id=1,QueryId=1,AccountId=1,Content= "Please click to log in the navbar and then choose “Need an account? Sign up!” to register your account.", CreatedAt=DateTime.Now.AddDays(-1) },
                new Reply {Id=2,QueryId=2,AccountId=2,Content= "Please click to login in the navbar. The login screen is displayed. Enter your email and password to login.", CreatedAt=DateTime.Now},
                new Reply {Id=3,QueryId=3,AccountId=3,Content= "Frist, you need to login. Please click page in then navbar and choose the help center. Click “Create by you” and then click to new issue to raise your quesiton. You can also answered the question which you want.", CreatedAt=DateTime.Now},
                new Reply {Id=4,QueryId=4,AccountId=4,Content= "Click pages and choose invite your friend in the navbar. The invite your friend’s screen is display. You will enter your name, email, the content of the email which you want to send. Last step, you click the button “Send Email” to mail to your friend.", CreatedAt=DateTime.Now.AddDays(-1)}
                );
        }
    }
}
