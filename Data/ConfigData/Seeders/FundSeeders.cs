using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class FundSeeders
    {
        public FundSeeders(ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Fund>().HasData(
                new Fund { Id=1,Total=100000,CurrentFund=5000,DonateId=1}
                );
        }
    }
}
