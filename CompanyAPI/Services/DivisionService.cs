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
            if (dbContext.Division.Any(c => c.Code == newDivision.Code))
            {
                throw new ArgumentException("A division with the given code already exists.");
            }
            newDivision.LeaderId = EnsureEmployeeExists(newDivision.LeaderId);
            newDivision.CompanyId = EnsureCompanyExists(newDivision.CompanyId);

            dbContext.Division.Add(newDivision);
            dbContext.SaveChanges();
            return newDivision;
        }

        public Division UpdateDivision(Division division)
        {
            var divisionInDb = dbContext.Division.Find(division.Id);

            if (divisionInDb == null)
            {
                throw new ArgumentException("Division not found.", nameof(division.Id));
            }

            division.LeaderId = EnsureEmployeeExists(division.LeaderId);
            division.CompanyId = EnsureCompanyExists(division.CompanyId);

            divisionInDb.Code = division.Code;
            divisionInDb.Name = division.Name;
            divisionInDb.LeaderId = division.LeaderId;
            divisionInDb.CompanyId = division.CompanyId;

            dbContext.SaveChanges();
            return divisionInDb;
        }

        public void DeleteDivision(int id)
        {
            var division = dbContext.Division.Find(id);

            if (division == null)
            {
                throw new ArgumentException("Division not found.");
            }

            dbContext.Division.Remove(division);
            dbContext.SaveChanges();
        }

        public Division GetDivisionById(int id)
        {
            var division = dbContext.Division.Find(id);

            if (division == null)
            {
                throw new ArgumentException("Division not found.");
            }

            return division;
        }


        private int? EnsureEmployeeExists(int? employeeId)
        {
            if (employeeId != 0)
            {
                if (!dbContext.Employee.Any(e => e.Id == employeeId))
                {
                    throw new ArgumentException("The employee with the given id does not exist.");
                } 
            } else
            {
                employeeId = null;
            }
            return employeeId;
            
        }

        private int EnsureCompanyExists(int companyId)
        {
            if (!dbContext.Company.Any(c => c.Id == companyId))
            {
                throw new ArgumentException("The company with the given id does not exist.");
            }
            return companyId;
        }

    }
}
