using Microsoft.EntityFrameworkCore;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data.ConfigData.Seeders
{
    public class AccountSeeders
    {
        public AccountSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account { Id=1,FullName="Nguyen Linh Phuong",Email="phuongnl@gmail.com",Password= "25d55ad283aa400af464c76d713c07ad", Address="Laos",Birthday= DateTime.Parse("1/1/1995 12:00:00 AM"),Phone="0941132369", RoleId=1},
                new Account { Id=2,FullName="Tran Cong Sang",Email="sangtrancong171196@gmail.com",Password= "25d55ad283aa400af464c76d713c07ad", Address="TPHCM",Birthday= DateTime.Parse("1/1/1996 12:00:00 AM") ,Phone = "0946963045", RoleId = 1 },
                new Account { Id=3,FullName="Lai Ngoc Thuy Tien",Email="tien@gmail.com",Password= "25d55ad283aa400af464c76d713c07ad", Address="TPHCM",Birthday= DateTime.Parse("1/1/1996 12:00:00 AM") ,Phone = "0125963999", RoleId = 1 },
                new Account { Id=4,FullName="Phung Van Phuc",Email="phuc@gmail.com",Password= "25d55ad283aa400af464c76d713c07ad", Address="TPHCM",Birthday= DateTime.Parse("1/1/1996 12:00:00 AM") ,Phone = "0125963999", RoleId = 1 },
                new Account { Id=5,FullName="Vu Bui Minh Hieu",Email="hieu1@gmail.com",Password= "25d55ad283aa400af464c76d713c07ad", Address="TPHCM",Birthday= DateTime.Parse("1/1/1996 12:00:00 AM") ,Phone = "0125963999", RoleId = 1 },
                new Account { Id=6,FullName="Sang user",Email="trancongsang.a1.vd.2014@gmail.com",Password= "25d55ad283aa400af464c76d713c07ad", Address="TPHCM",Birthday= DateTime.Parse("1/1/1996 12:00:00 AM") ,Phone = "0125963999", RoleId = 2 }
            );
        }
    }
}
