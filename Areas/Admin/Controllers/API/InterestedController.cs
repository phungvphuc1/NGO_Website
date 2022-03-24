using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class InterestedController : ControllerBase
    {
        private readonly DatabaseContext context;
        public InterestedController(DatabaseContext context)
        {
            this.context = context;
        }
        [HttpGet("{accountId}")]
        public IActionResult GetOne(int i)
        {
            return Ok(context.GetInteresteds.ToList());
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(context.GetInteresteds.ToList());
        }
        [HttpPost]
        public async Task<bool> Create([FromForm] Interested interested)
        {
            var model = context.GetInteresteds.SingleOrDefault(x => x.AccountId == interested.AccountId && x.ProgramId == interested.ProgramId);
            if (model==null)
            {
                context.GetInteresteds.Add(interested);
                context.SaveChanges();
                return await Task.FromResult(true);
            }
            else
            {
                context.GetInteresteds.Remove(model);
                context.SaveChanges();
                return await Task.FromResult(false);
            }
            
        }
    }
}
