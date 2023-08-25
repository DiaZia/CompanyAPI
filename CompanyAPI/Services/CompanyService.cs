using CompanyAPI.Data;
using CompanyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CompanyAPI.Services
{
    public class CompanyService
    {
        private readonly ApiContext _dbContext;
        public CompanyService(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Company>> GetAllCompanies()
        {
            return await _dbContext.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyById(int companyId)
        {
            return await _dbContext.Companies.FindAsync(companyId);
        }

        public async Task CreateCompany(Company company)
        {
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();
        }
    }
}
