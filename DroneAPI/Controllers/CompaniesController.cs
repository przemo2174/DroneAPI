using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet(Name = "GetCompanies")]
        public IActionResult GetCompanies(CompaniesQueryParameters companiesQueryParameters)
        {
            var collectionBeforePaging = _dbContext.Companies.OrderBy(x => x.Name);

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

            return Ok(pagedList);
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
                        pageSize = companiesQueryParameters.PageSize
                    });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetCompanies", new
                    {
                        pageNumber = companiesQueryParameters.PageNumber + 1,
                        pageSize = companiesQueryParameters.PageSize
                    });
                default:
                    return _urlHelper.Link("GetCompanies", new
                    {
                        pageNumber = companiesQueryParameters.PageNumber,
                        pageSize = companiesQueryParameters.PageSize
                    });
            }
        }
    }
}