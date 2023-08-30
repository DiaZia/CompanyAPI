using CompanyAPI.Data;
using CompanyAPI.Models;
using System.ComponentModel.Design;

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
            newDepartment.ProjectId = (int)EnsureProjectExists(newDepartment.ProjectId);

            dbContext.Department.Add(newDepartment);
            dbContext.SaveChanges();
            return newDepartment;
        }

        public Department UpdateDepartment(int id, string? code, string? name, int? leaderId, int? projectId)
        {
            var departmentInDb = dbContext.Department.Find(id);

            if (departmentInDb == null)
            {
                throw new ArgumentException("Department not found.");
            }

            leaderId = EnsureEmployeeExists(leaderId);
            projectId = EnsureProjectExists(projectId);


            if (code != null)
            {
                departmentInDb.Code = code;
            }
            if (name != null)
            {
                departmentInDb.Name = name;
            }
            if (leaderId != null)
            {
                departmentInDb.LeaderId = leaderId;
            }
            if (projectId != null)
            {
                departmentInDb.ProjectId = (int)projectId;
            }

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
            if (employeeId != null)
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
            }
            return employeeId;
        }

        private int? EnsureProjectExists(int? projectId)
        {
            if (projectId != null)
            {
                if (!dbContext.Project.Any(d => d.Id == projectId))
                {
                    throw new ArgumentException("The project with the given id does not exist.");
                }
            }
            return projectId;
        }
    }
}
