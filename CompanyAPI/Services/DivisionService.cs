using CompanyAPI.Data;
using CompanyAPI.Models;

namespace CompanyAPI.Services
{
    public class DivisionService
    {
        private readonly ApiContext dbContext;
        public DivisionService(ApiContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Division CreateDivision(Division newDivision)
        {
            EnsureEmployeeExists(newDivision.LeaderId);
            EnsureCompanyExists(newDivision.CompanyId);

            dbContext.Divisions.Add(newDivision);
            dbContext.SaveChanges();
            return newDivision;
        }

        public Division UpdateDivision(Division division)
        {
            var divisionInDb = dbContext.Divisions.Find(division.Id);

            if (divisionInDb == null)
            {
                throw new ArgumentException("Division not found.", nameof(division.Id));
            }

            EnsureEmployeeExists(division.LeaderId);
            EnsureCompanyExists(division.CompanyId);

            divisionInDb.Code = division.Code;
            divisionInDb.Name = division.Name;
            divisionInDb.LeaderId = division.LeaderId;
            divisionInDb.CompanyId = division.CompanyId;

            dbContext.SaveChanges();
            return divisionInDb;
        }

        public void DeleteDivision(int id)
        {
            var division = dbContext.Divisions.Find(id);

            if (division == null)
            {
                throw new ArgumentException("Division not found.", nameof(id));
            }

            dbContext.Divisions.Remove(division);
            dbContext.SaveChanges();
        }

        public Division GetDivisionById(int id)
        {
            var division = dbContext.Divisions.Find(id);

            if (division == null)
            {
                throw new ArgumentException("Division not found.", nameof(id));
            }

            return division;
        }


        private void EnsureEmployeeExists(int? employeeId)
        {
            if (!dbContext.Employees.Any(e => e.Id == employeeId))
            {
                throw new ArgumentException("The employee with the given id does not exist.");
            }
        }

        private void EnsureCompanyExists(int companyId)
        {
            if (!dbContext.Companies.Any(c => c.Id == companyId))
            {
                throw new ArgumentException("The company with the given id does not exist.");
            }
        }

    }
}
