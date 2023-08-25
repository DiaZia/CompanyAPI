using CompanyAPI.Data;
using CompanyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly ApiContext _context;

        public ProjectController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(Project project)
        {
            if (project.Id == 0)
            {
                _context.Projects.Add(project);
            }
            else
            {
                var projectInDb = _context.Projects.Find(project.Id);

                if (projectInDb == null)
                {
                    return new JsonResult(NotFound());
                }

                projectInDb = project;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(project));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Projects.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Projects.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
