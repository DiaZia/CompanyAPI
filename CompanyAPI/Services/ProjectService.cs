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
            EnsureEmployeeExists(newProject.LeaderId);
            EnsureDivisionExists(newProject.DivisionId);

            dbContext.Projects.Add(newProject);
            dbContext.SaveChanges();
            return newProject;
        }

        public Project UpdateProject(Project project)
        {
            var projectInDb = dbContext.Projects.Find(project.Id);

            if (projectInDb == null)
            {
                throw new ArgumentException("Project not found.", nameof(project.Id));
            }

            EnsureEmployeeExists(project.LeaderId);
            EnsureDivisionExists(project.DivisionId);

            projectInDb.Code = project.Code;
            projectInDb.Name = project.Name;
            projectInDb.LeaderId = project.LeaderId;
            projectInDb.DivisionId = project.DivisionId;

            dbContext.SaveChanges();
            return projectInDb;
        }

        public void DeleteProject(int id)
        {
            var project = dbContext.Projects.Find(id);

            if (project == null)
            {
                throw new ArgumentException("Project not found.", nameof(id));
            }

            dbContext.Projects.Remove(project);
            dbContext.SaveChanges();
        }

        public Project GetProjectById(int id)
        {
            var project = dbContext.Projects.Find(id);

            if (project == null)
            {
                throw new ArgumentException("Project not found.", nameof(id));
            }

            return project;
        }


        private void EnsureEmployeeExists(int? employeeId)
        {
            if (!dbContext.Employees.Any(e => e.Id == employeeId))
            {
                throw new ArgumentException("The employee with the given id does not exist.");
            }
        }

        private void EnsureDivisionExists(int divisionId)
        {
            if (!dbContext.Divisions.Any(d => d.Id == divisionId))
            {
                throw new ArgumentException("The division with the given id does not exist.");
            }
        }
    }
}
