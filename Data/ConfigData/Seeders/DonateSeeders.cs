using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class DonateSeeders
    {
        public DonateSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donate>().HasData(
                new Donate {Id=1,AccountId=6,CategoryId=2,PartnerId=2,ProgramId=1,Amount=1000 ,CreatedAt=DateTime.Now.AddDays(-6)},
                new Donate {Id=2,AccountId=6,CategoryId=3,PartnerId=3,Amount=2000, CreatedAt = DateTime.Now.AddDays(-5) },
                new Donate {Id=3,AccountId=6,CategoryId=2,ProgramId=2,PartnerId=2,Amount=20000 , CreatedAt = DateTime.Now.AddDays(-3) },             
                new Donate {Id=5,AccountId=6,CategoryId=4,ProgramId=4,PartnerId=4,Amount=7000, CreatedAt = DateTime.Now }
                );
        }
    }
}
