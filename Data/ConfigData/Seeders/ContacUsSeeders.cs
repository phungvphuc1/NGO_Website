using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class ContacUsSeeders
    {
        public ContacUsSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactUs>().HasData(
                 new ContactUs { Id=1,Name="sang",Email="trancongsang.a1.vd.2014@gmail.com",Content="good website",Phone="0946963045"},
                 new ContactUs { Id=2,Name="nam",Email="nam@gmail.com",Content="good job",Phone="0946963335"}



            );
        }
    }
}
