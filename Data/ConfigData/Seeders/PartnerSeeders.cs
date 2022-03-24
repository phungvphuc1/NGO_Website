using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class PartnerSeeders
    {
        public PartnerSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Partner>().HasData(
                new Partner() {Id=1,OrgName= "Crescent Moon Charity Fund", Email= "Charity@gmail.com", Phone= "0925695555", Address= "Viet Nam", CategoryId=1,Logo= "4d4ce557-f420-46a6-b7bd-abe59a229bf4.png" },
                new Partner() {Id=2,OrgName= "MSD . organization", Email= "msd@email.com", Phone= "8494633356", Address="Viet Nam",CategoryId=2,Logo= "2275b1b0-4acd-4177-842c-51784e4158bb.jpg" },
                new Partner() {Id=3,OrgName= "Operation Smile", Email= "smile@gmail.com", Phone= "0946963056", Address="Viet Nam",CategoryId=3,Logo= "de1be3a6-8d06-40c7-a09b-190458ca920b.jpg" },
                new Partner() {Id=4,OrgName= "Power 2000", Email= "2000power@gmail.com", Phone= "0948633555", Address="Viet Nam",CategoryId=4,Logo= "4c35d10b-7741-4a1e-ad47-0518064564eb.jpg" },
                new Partner() {Id=5,OrgName= "Flower and Sharing Charity Fund", Email= "flower@gmail.com", Phone= "8494633356", Address="Viet Nam",CategoryId=2,Logo= "868a1bbd-c9d3-4802-842d-12a7bf141f68.jpg" }
                );
        }
    }
}
