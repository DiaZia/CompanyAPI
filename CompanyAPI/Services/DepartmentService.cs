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
            if (dbContext.Department.Any(c => c.Code == newDepartment.Code))
            {
                throw new ArgumentException("A department with the given code already exists.");
            }
            newDepartment.LeaderId = EnsureEmployeeExists(newDepartment.LeaderId);
            newDepartment.ProjectId = EnsureProjectExists(newDepartment.ProjectId);

            dbContext.Department.Add(newDepartment);
            dbContext.SaveChanges();
            return newDepartment;
        }

        public Department UpdateDepartment(Department department)
        {
            var departmentInDb = dbContext.Department.Find(department.Id);

            if (departmentInDb == null)
            {
                throw new ArgumentException("Department not found.", nameof(department.Id));
            }

            department.LeaderId = EnsureEmployeeExists(department.LeaderId);
            department.ProjectId = EnsureProjectExists(department.ProjectId);

            departmentInDb.Code = department.Code;
            departmentInDb.Name = department.Name;
            departmentInDb.LeaderId = department.LeaderId;
            departmentInDb.ProjectId = department.ProjectId;

            dbContext.SaveChanges();
            return departmentInDb;
        }

        public void DeleteDepartment(int id)
        {
            var department = dbContext.Department.Find(id);

            if (department == null)
            {
                throw new ArgumentException("Department not found.");
            }

            dbContext.Department.Remove(department);
            dbContext.SaveChanges();
        }

        public Department GetDepartmentById(int id)
        {
            var department = dbContext.Department.Find(id);

            if (department == null)
            {
                throw new ArgumentException("Department not found.");
            }

            return department;
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

        private int EnsureProjectExists(int projectId)
        {
            if (!dbContext.Project.Any(d => d.Id == projectId))
            {
                throw new ArgumentException("The project with the given id does not exist.");
            }
            return projectId;
        }
    }
}
