using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public static class AppSeeders
    {
        public static void CallSeeder(ModelBuilder modelBuilder)
        {
            new RoleSeeders(modelBuilder);
            new AccountSeeders(modelBuilder);
            new ContacUsSeeders(modelBuilder);
            new PartnerSeeders(modelBuilder);
            new DonateCategorySeeders(modelBuilder);
            new DonateSeeders(modelBuilder);
            new ProgramsSeeders(modelBuilder);
            new InterestedSeeders(modelBuilder);          
            new PhotosSeeders(modelBuilder);       
            new QuerySeeders(modelBuilder);
            new ReplySeeders(modelBuilder);           
            new FundSeeders(modelBuilder);

        }
    }
}
