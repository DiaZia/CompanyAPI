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

        public Company UpdateCompany(int id, string? code, string? name, int? directorId)
        {
            var companyInDb = dbContext.Company.Find(id);

            if (companyInDb == null)
            {
                throw new ArgumentException("Company not found.");
            }
            if (directorId != null)
            {
                if (directorId != 0)
                {
                    if (!dbContext.Employee.Any(e => e.Id == directorId))
                    {
                        throw new ArgumentException("The employee with the given id does not exist.");
                    }
                    companyInDb.DirectorId = directorId; 
                }
                else
                {
                    directorId = null;
                }
            }
            if (code != null)
            {
                companyInDb.Code = code;
            }
            if (name != null)
            {
                companyInDb.Name = name;
            }
            

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
