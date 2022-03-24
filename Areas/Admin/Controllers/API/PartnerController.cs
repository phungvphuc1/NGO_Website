using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NGOWebApp.Data;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly DatabaseContext context;
        public PartnerController(DatabaseContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public  IActionResult GetAll()
        {
            var model = context.GetPartners.Include(x => x.GetDonateCategory);
            return Ok(JsonConvert.SerializeObject(model, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })) ;
        }
        [HttpGet("{name}")]
        public IActionResult GetOne(string name)
        {
            try
            {
                var model = from p in context.GetPartners.Include(x => x.GetDonateCategory)
                            join d in context.GetDonates on p.Id equals d.PartnerId
                            where p.OrgName.Equals(name)
                            select new { Partner = p, TotalMoney = p.GetDonates.Select(a => a.Amount).Sum(), CurrentMoney = p.GetDonates.Where(d => d.Status == 1).Select(a => a.Amount).Sum() };
                return Ok(JsonConvert.SerializeObject(model.First(), Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
            }
            catch (Exception)
            {

                return Ok(null);
            }
           
        }
    }
}
