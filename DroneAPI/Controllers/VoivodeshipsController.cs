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
    [Route("api/voivodeships")]
    public class VoivodeshipsController : Controller
    {
        private readonly DroneDatabaseContext _dbContext;

        public VoivodeshipsController(DroneDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetVoivodeships()
        {
            List<string> voivodeships = _dbContext.Companies.Select(x => x.Voivodeship).Distinct().OrderBy(x => x).ToList();
            return Ok(voivodeships);
        }

        [HttpGet("{voivodeship}/cities")]
        public IActionResult GetCitiesByVoivodeship(string voivodeship)
        {
            voivodeship = voivodeship.Trim().ToLowerInvariant();

            List<string> cities = _dbContext.Companies.Where(x => x.Voivodeship.ToLowerInvariant() == voivodeship)
                .Select(x => x.City).Distinct().OrderBy(x => x).ToList();

            return Ok(cities);
        }
    }
}