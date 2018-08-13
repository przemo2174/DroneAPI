using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DroneAPI.DTO;
using DroneAPI.Helpers;
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
        private readonly IUrlHelper _urlHelper;

        public CompaniesController(DroneDatabaseContext dbContext, IUrlHelper urlHelper)
        {
            _dbContext = dbContext;
            _urlHelper = urlHelper;
        }

        //[HttpGet]
        //public IEnumerable<Company> GetCompanies()
        //{
        //    return _dbContext.Companies.OrderBy(x => x.Name);
        //}

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            Company company = _dbContext.Companies.SingleOrDefault(x => x.Id == id);

            if (company != null)
            {
                return Ok(company);
            }

            return NotFound();
        }

        [HttpGet(Name = "GetCompanies")]
        public IActionResult GetCompanies(CompaniesQueryParameters companiesQueryParameters)
        {
            var collectionBeforePaging = _dbContext.Companies.OrderBy(x => x.Name).AsQueryable();

            if (!string.IsNullOrEmpty(companiesQueryParameters.Name))
            {
                string nameForWhereClause = companiesQueryParameters.Name.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Name.ToLowerInvariant().Contains(nameForWhereClause));
            }

            if (!string.IsNullOrEmpty(companiesQueryParameters.Nip))
            {
                string nipForWhereClause = companiesQueryParameters.Nip.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Nip.ToLowerInvariant().Contains(nipForWhereClause));
            }

            if (!string.IsNullOrEmpty(companiesQueryParameters.Voivodeship))
            {
                string voivodeshipForWhereClause = companiesQueryParameters.Voivodeship.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Voivodeship.ToLowerInvariant().Contains(voivodeshipForWhereClause));
            }

            if (!string.IsNullOrEmpty(companiesQueryParameters.City))
            {
                string cityForWhereClause = companiesQueryParameters.City.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.City.ToLowerInvariant().Contains(cityForWhereClause));
            }



            PagedList<Company> pagedList = PagedList<Company>.Create(collectionBeforePaging, companiesQueryParameters.PageNumber, companiesQueryParameters.PageSize);

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
                data = pagedList
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

            var company = Mapper.Map<Company>(companyDto);

            _dbContext.Companies.Add(company);

            try
            {
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "A problem happened with handling you request.");
            }

            return CreatedAtRoute("GetById", new {id = company.Id}, company);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var company = _dbContext.Companies.SingleOrDefault(x => x.Id == id);

            if (company == null)
                return NotFound();


            _dbContext.Companies.Remove(company);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
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