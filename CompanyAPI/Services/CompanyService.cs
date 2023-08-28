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
            if (dbContext.Company.Any(c => c.Code == newCompany.Code))
            {
                throw new ArgumentException("A company with the given code already exists.");
            }
            if (newCompany.DirectorId != 0)
            {
                if (dbContext.Employee.Any(e => e.Id == newCompany.DirectorId))
                {
                    dbContext.Company.Add(newCompany);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("The employee with the given id does not exist.");
                }
            }
            else
            {
                newCompany.DirectorId = null;
                dbContext.Company.Add(newCompany);
                dbContext.SaveChanges();
            }
            return newCompany;
        }

        public Company UpdateCompany(Company company)
        {
            var companyInDb = dbContext.Company.Find(company.Id);

            if (companyInDb == null)
            {
                throw new ArgumentException("Company not found.", nameof(company.Id));
            }
            if (company.DirectorId != 0)
            {
                if (!dbContext.Employee.Any(e => e.Id == company.DirectorId))
                {
                    throw new ArgumentException("The employee with the given id does not exist.");
                }
            } else
            {
                company.DirectorId = null;
            }

            companyInDb.Code = company.Code;
            companyInDb.Name = company.Name;
            companyInDb.DirectorId = company.DirectorId;

            dbContext.SaveChanges();
            return companyInDb;
        }

        public void DeleteCompany(int id)
        {
            var company = dbContext.Company.Find(id);

            if (company == null)
            {
                throw new ArgumentException("Company not found.");
            }

            dbContext.Company.Remove(company);
            dbContext.SaveChanges();
        }

        public Company GetCompanyById(int id)
        {
            var company = dbContext.Company.Find(id);

            if (company == null)
            {
                throw new ArgumentException("Company not found.");
            }

            return company;
        }
    }
}
