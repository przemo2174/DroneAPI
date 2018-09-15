using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DroneAPI.DTO;
using DroneAPI.Helpers;
using DroneAPI.Models;
using DronesApp.API.DTO;
using DronesApp.API.Extensions;
using DronesApp.API.Repositories;
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
        private readonly IUrlHelper _urlHelper;
        private readonly ICompaniesRepository _companiesRepository;

        public CompaniesController(IUrlHelper urlHelper, ICompaniesRepository companiesRepository)
        {
            _urlHelper = urlHelper;
            _companiesRepository = companiesRepository;
        }

        //[HttpGet]
        //public IEnumerable<Company> GetCompanies()
        //{
        //    return _dbContext.Companies.OrderBy(x => x.Name);
        //}

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            Company company = _companiesRepository.GetCompanyById(id);

            if (company != null)
            {
                CompanyDto companyDto = Mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }

            return NotFound();
        }

        [HttpGet(Name = "GetCompanies")]
        public IActionResult GetCompanies(CompaniesQueryParameters companiesQueryParameters)
        {
          
            PagedList<Company> pagedList = _companiesRepository.GetCompanies(companiesQueryParameters);

            var previousPageLink = pagedList.HasPrevious
                ? CreateCompaniesResourceUri(companiesQueryParameters, ResourceUriType.PreviousPage)
                : null;

            var nextPageLink = pagedList.HasNext
                ? CreateCompaniesResourceUri(companiesQueryParameters, ResourceUriType.NextPage)
                : null;

            var paginationMetadata = new
            {
                totalCount = pagedList.TotalCount,
                pageSize = pagedList.PageSize,
                currentPage = pagedList.CurrentPage,
                totalPages = pagedList.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            var response = new
            {
                metadata = paginationMetadata,
                data = pagedList.ToMappedPagedList<Company, CompanyDto>()
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CreateCompanyDto companyDto)
        {
            if (companyDto == null)
            {
                return BadRequest();
            }

            Company company = Mapper.Map<Company>(companyDto);

            _companiesRepository.AddCompany(company);

            if (!_companiesRepository.Save())
            {
                return StatusCode(500, "A problem happened with handling you request.");
            }

            return CreatedAtRoute("GetById", new { id = company.Id }, company);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            Company company = _companiesRepository.GetCompanyById(id);

            if (company == null)
            {
                return NotFound();
            }

            _companiesRepository.DeleteCompany(company);

            if (!_companiesRepository.Save())
            {
                return StatusCode(500, "A problem happened with handling you request.");
            }

            return NoContent();          
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany(int id, [FromBody] UpdateCompanyDto companyForUpdate)
        {
            if (companyForUpdate == null)
            {
                return BadRequest();
            }

            Company company = _companiesRepository.GetCompanyById(id);

            if (company == null)
            {
                return NotFound();
            }

            Mapper.Map(companyForUpdate, company);

            if (!_companiesRepository.Save())
            {
                return StatusCode(500, "A problem happened with handling you request.");
            }

            return NoContent();          
        }

        private string CreateCompaniesResourceUri(CompaniesQueryParameters companiesQueryParameters,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetCompanies", new
                    {
                        pageNumber = companiesQueryParameters.PageNumber - 1,
                        pageSize = companiesQueryParameters.PageSize,
                        name = companiesQueryParameters.Name,
                        nip = companiesQueryParameters.Nip,
                        voivodeship = companiesQueryParameters.Voivodeship,
                        city = companiesQueryParameters.City
                    });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetCompanies", new
                    {
                        pageNumber = companiesQueryParameters.PageNumber + 1,
                        pageSize = companiesQueryParameters.PageSize,
                        name = companiesQueryParameters.Name,
                        nip = companiesQueryParameters.Nip,
                        voivodeship = companiesQueryParameters.Voivodeship,
                        city = companiesQueryParameters.City
                    });
                default:
                    return _urlHelper.Link("GetCompanies", new
                    {
                        pageNumber = companiesQueryParameters.PageNumber,
                        pageSize = companiesQueryParameters.PageSize,
                        name = companiesQueryParameters.Name,
                        nip = companiesQueryParameters.Nip,
                        voivodeship = companiesQueryParameters.Voivodeship,
                        city = companiesQueryParameters.City
                    });
            }
        }
    }
}