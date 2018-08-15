using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroneAPI.Helpers;
using DroneAPI.Models;

namespace DronesApp.API.Repositories
{
    public interface ICompaniesRepository
    {
        PagedList<Company> GetCompanies(CompaniesQueryParameters companiesQueryParameters);
        Company GetCompanyById(int id);
        void AddCompany(Company companyToCreate);
        void DeleteCompany(Company companyToDelete);
        bool CompanyExists(int id);
        bool Save();
    }
}
