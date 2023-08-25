using CompanyAPI.Data;
using CompanyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly ApiContext _context;

        public DepartmentController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(Department department)
        {
            if (department.Id == 0)
            {
                _context.Departments.Add(department);
            }
            else
            {
                var departmentInDb = _context.Departments.Find(department.Id);

                if (departmentInDb == null)
                {
                    return new JsonResult(NotFound());
                }

                departmentInDb = department;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(department));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Departments.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Departments.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
