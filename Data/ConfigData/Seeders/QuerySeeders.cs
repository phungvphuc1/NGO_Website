using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class QuerySeeders
    {
        public QuerySeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Query>().HasData(
                new Query { Id=1,Title= "How to register your account?", Content= "Please click to log in the navbar and then choose “Need an account? Sign up!” to register your account.", Status=4, AccountId=6,CreatedAt=DateTime.Now.AddDays(-3)},
                new Query { Id=2,Title= "How to Login your account?", Content= "Please click to login in the navbar. The login screen is displayed. Enter your email and password to login.", Status=4, AccountId=6,CreatedAt=DateTime.Now.AddDays(-4)},
                new Query { Id=3,Title= "How to create new issue in help center?", Content= "Frist, you need to login. Please click page in then navbar and choose the help center. Click “Create by you” and then click to new issue to raise your quesiton. You can also answered the question which you want.", Status=4, AccountId=6,CreatedAt=DateTime.Now.AddDays(-2)},
                new Query { Id=4,Title= "How to invite your friend by email?", Content= "Click pages and choose invite your friend in the navbar. The invite your friend’s screen is display. You will enter your name, email, the content of the email which you want to send. Last step, you click the button “Send Email” to mail to your friend.", Status=4, AccountId=6,CreatedAt=DateTime.Now.AddDays(-1)}
                );
        }
    }
}
