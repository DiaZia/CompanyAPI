using CompanyAPI.Data;
using CompanyAPI.Models;

namespace CompanyAPI.Services
{
    public class DepartmentService
    {
        private readonly ApiContext dbContext;
        public DepartmentService(ApiContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Department CreateDepartment(Department newDepartment)
        {
            EnsureEmployeeExists(newDepartment.LeaderId);
            EnsureProjectExists(newDepartment.ProjectId);

            dbContext.Departments.Add(newDepartment);
            dbContext.SaveChanges();
            return newDepartment;
        }

        public Department UpdateDepartment(Department department)
        {
            var departmentInDb = dbContext.Departments.Find(department.Id);

            if (departmentInDb == null)
            {
                throw new ArgumentException("Department not found.", nameof(department.Id));
            }

            EnsureEmployeeExists(department.LeaderId);
            EnsureProjectExists(department.ProjectId);

            departmentInDb.Code = department.Code;
            departmentInDb.Name = department.Name;
            departmentInDb.LeaderId = department.LeaderId;
            departmentInDb.ProjectId = department.ProjectId;

            dbContext.SaveChanges();
            return departmentInDb;
        }

        public void DeleteDepartment(int id)
        {
            var department = dbContext.Departments.Find(id);

            if (department == null)
            {
                throw new ArgumentException("Department not found.", nameof(id));
            }

            dbContext.Departments.Remove(department);
            dbContext.SaveChanges();
        }

        public Department GetDepartmentById(int id)
        {
            var department = dbContext.Departments.Find(id);

            if (department == null)
            {
                throw new ArgumentException("Department not found.", nameof(id));
            }

            return department;
        }


        private void EnsureEmployeeExists(int? employeeId)
        {
            if (!dbContext.Employees.Any(e => e.Id == employeeId))
            {
                throw new ArgumentException("The employee with the given id does not exist.");
            }
        }

        private void EnsureProjectExists(int projectId)
        {
            if (!dbContext.Projects.Any(d => d.Id == projectId))
            {
                throw new ArgumentException("The project with the given id does not exist.");
            }
        }
    }
}
