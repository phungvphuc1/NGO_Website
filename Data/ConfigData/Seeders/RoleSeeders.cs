using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class RoleSeeders
    {
        public RoleSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(
                new Roles {Id=1, Role=1,Description="Admin"},
                new Roles {Id=2, Role=2,Description="User"}
                );
        }
    }
}
