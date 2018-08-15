using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroneAPI.Helpers;
using DroneAPI.Models;

namespace DronesApp.API.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly DroneDatabaseContext _dbContext;

        public CompaniesRepository(DroneDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public PagedList<Company> GetCompanies(CompaniesQueryParameters companiesQueryParameters)
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
                    .Where(x => x.Nip != null && x.Nip.ToLowerInvariant().Contains(nipForWhereClause));
            }

            if (!string.IsNullOrEmpty(companiesQueryParameters.Voivodeship))
            {
                string voivodeshipForWhereClause = companiesQueryParameters.Voivodeship.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.Voivodeship != null && x.Voivodeship.ToLowerInvariant().Contains(voivodeshipForWhereClause));
            }

            if (!string.IsNullOrEmpty(companiesQueryParameters.City))
            {
                string cityForWhereClause = companiesQueryParameters.City.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(x => x.City != null && x.City.ToLowerInvariant().Contains(cityForWhereClause));
            }
            PagedList<Company> pagedList = PagedList<Company>.Create(collectionBeforePaging, companiesQueryParameters.PageNumber, companiesQueryParameters.PageSize);

            return pagedList;
        }

        public Company GetCompanyById(int id)
        {
            return _dbContext.Companies.SingleOrDefault(x => x.Id == id);
        }

        public void AddCompany(Company companyToCreate)
        {
            _dbContext.Companies.Add(companyToCreate);
        }

        public void DeleteCompany(Company companyToDelete)
        {
            _dbContext.Companies.Remove(companyToDelete);
        }

        public bool CompanyExists(int id)
        {
            return GetCompanyById(id) != null;
        }

        public bool Save()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
