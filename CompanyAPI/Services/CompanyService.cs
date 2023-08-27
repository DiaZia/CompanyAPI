using CompanyAPI.Data;
using CompanyAPI.Models;

namespace CompanyAPI.Services
{
    public class CompanyService
    {
        private readonly ApiContext dbContext;

        public CompanyService(ApiContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Company CreateCompany(Company newCompany)
        {
            if (dbContext.Employees.Any(e => e.Id == newCompany.DirectorId))
            {
                dbContext.Companies.Add(newCompany);
                dbContext.SaveChanges();
                return newCompany;
            }
            else
            {
                throw new ArgumentException("The employee with the given id does not exist.");
            }
        }

        public Company UpdateCompany(Company company)
        {
            var companyInDb = dbContext.Companies.Find(company.Id);

            if (companyInDb == null)
            {
                throw new ArgumentException("Company not found.", nameof(company.Id));
            }

            if (!dbContext.Employees.Any(e => e.Id == company.DirectorId))
            {
                throw new ArgumentException("The employee with the given id does not exist.");
            }

            companyInDb.Code = company.Code;
            companyInDb.Name = company.Name;
            companyInDb.DirectorId = company.DirectorId;

            dbContext.SaveChanges();
            return companyInDb;
        }

        public void DeleteCompany(int id)
        {
            var company = dbContext.Companies.Find(id);

            if (company == null)
            {
                throw new ArgumentException("Company not found.", nameof(id));
            }

            dbContext.Companies.Remove(company);
            dbContext.SaveChanges();
        }

        public Company GetCompanyById(int id)
        {
            var company = dbContext.Companies.Find(id);

            if (company == null)
            {
                throw new ArgumentException("Company not found.", nameof(id));
            }

            return company;
        }
    }
}
