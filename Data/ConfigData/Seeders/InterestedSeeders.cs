using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class InterestedSeeders
    {
        public InterestedSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Interested>().HasData(
                new Interested { Id=1,AccountId=6,ProgramId=1,PartnerId=1}
                );
        }
    }
}
