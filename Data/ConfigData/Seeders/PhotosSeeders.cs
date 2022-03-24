using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class PhotosSeeders
    {
        public PhotosSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photos>().HasData(
                new Photos { Id=1,ProgramId=1,Photo= "images/AlbumProgram/program1-img1.jpg" },
                new Photos { Id=2,ProgramId=1,Photo= "images/AlbumProgram/program1-img2.jpg" }
                );
        }
    }
}
