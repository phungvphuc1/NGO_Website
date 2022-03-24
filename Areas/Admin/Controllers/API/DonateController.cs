using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NGOWebApp.Data;
using NGOWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonateController : ControllerBase
    {
        private readonly DatabaseContext context;
        public DonateController(DatabaseContext context)
        {
            this.context = context;
        }
        [HttpGet("{name}")]
        public IActionResult GetOne(string name)
        {

            var model = from p in context.GetDonates.Include(x => x.GetAccount).OrderByDescending(x=>x.CreatedAt)
                        join d in context.GetPartners on p.PartnerId equals d.Id
                        where d.OrgName.Equals(name)
                        select new { p, d };

            //Partner partner = context.GetPartners.FirstOrDefault(x => x.OrgName.Equals(name));
            //var model = from d in context.GetDonates select d;
            var result = model.Select(x => new DonateVM
            {
                Donate = x.p,
                TotalMoney = x.d.GetDonates.Select(a => a.Amount).Sum(),
                CurrentMoney = x.d.GetDonates.Where(d => d.Status == 1).Select(d => d.Amount).Sum()
            });
            return Ok(JsonConvert.SerializeObject(result, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));

        }
    }
}
