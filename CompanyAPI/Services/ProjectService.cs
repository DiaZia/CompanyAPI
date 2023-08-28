using CompanyAPI.Data;
using CompanyAPI.Models;
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
            newProject.DivisionId = EnsureDivisionExists(newProject.DivisionId);

            dbContext.Project.Add(newProject);
            dbContext.SaveChanges();
            return newProject;
        }

        public Project UpdateProject(Project project)
        {
            var projectInDb = dbContext.Project.Find(project.Id);

            if (projectInDb == null)
            {
                throw new ArgumentException("Project not found.", nameof(project.Id));
            }

            project.LeaderId = EnsureEmployeeExists(project.LeaderId);
            project.DivisionId = EnsureDivisionExists(project.DivisionId);

            projectInDb.Code = project.Code;
            projectInDb.Name = project.Name;
            projectInDb.LeaderId = project.LeaderId;
            projectInDb.DivisionId = project.DivisionId;

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
            return employeeId;
        }

        private int EnsureDivisionExists(int divisionId)
        {
            if (!dbContext.Division.Any(d => d.Id == divisionId))
            {
                throw new ArgumentException("The division with the given id does not exist.");
            }
            return divisionId;
        }
    }
}
