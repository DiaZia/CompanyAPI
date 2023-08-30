using CompanyAPI.Data;
using CompanyAPI.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Security.Principal;

namespace CompanyAPI.Services
{
    public class ProjectService
    {
        private readonly ApiContext dbContext;
        public ProjectService(ApiContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Project CreateProject(Project newProject)
        {
            if (dbContext.Project.Any(c => c.Code == newProject.Code))
            {
                throw new ArgumentException("A project with the given code already exists.");
            }
            newProject.LeaderId = EnsureEmployeeExists(newProject.LeaderId);
            newProject.DivisionId = (int)EnsureDivisionExists(newProject.DivisionId);

            dbContext.Project.Add(newProject);
            dbContext.SaveChanges();
            return newProject;
        }

        public Project UpdateProject(int id, string? code, string? name, int? leaderId, int? divisionId)
        {
            var projectInDb = dbContext.Project.Find(id);

            if (projectInDb == null)
            {
                throw new ArgumentException("Project not found.");
            }

            leaderId = EnsureEmployeeExists(leaderId);
            divisionId = EnsureDivisionExists(divisionId);

            if (code != null)
            {
                projectInDb.Code = code;
            }
            if (name != null)
            {
                projectInDb.Name = name;
            }
            if (leaderId != null)
            {
                projectInDb.LeaderId = leaderId;
            }
            if (divisionId != null)
            {
                projectInDb.DivisionId = (int)divisionId;
            }

            dbContext.SaveChanges();
            return projectInDb;
        }

        public void DeleteProject(int id)
        {
            var project = dbContext.Project.Find(id);

            if (project == null)
            {
                throw new ArgumentException("Project not found.");
            }

            dbContext.Project.Remove(project);
            dbContext.SaveChanges();
        }

        public Project GetProjectById(int id)
        {
            var project = dbContext.Project.Find(id);

            if (project == null)
            {
                throw new ArgumentException("Project not found.");
            }

            return project;
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
                }
                else
                {
                    employeeId = null;
                }
            }
            return employeeId;
        }

        private int? EnsureDivisionExists(int? divisionId)
        {
            if (divisionId != null)
            {
                if (!dbContext.Division.Any(d => d.Id == divisionId))
                {
                    throw new ArgumentException("The division with the given id does not exist.");
                }
            }
            return divisionId;
        }
    }
}
