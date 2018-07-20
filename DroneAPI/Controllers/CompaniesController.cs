using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroneAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DroneAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/companies")]
    public class CompaniesController : Controller
    {
        private readonly DroneDatabaseContext _dbContext;

        public CompaniesController(DroneDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Company> GetCompanies()
        {
            return _dbContext.Companies;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Company company = _dbContext.Companies.SingleOrDefault(x => x.Id == id);

            if (company != null)
            {
                return Ok(company);
            }

            return NotFound();
        }
    }
}