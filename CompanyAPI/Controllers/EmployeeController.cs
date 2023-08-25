using CompanyAPI.Data;
using CompanyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApiContext _context;

        public EmployeeController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(Employee employee)
        {
            if (employee.Id == 0)
            {
                _context.Employees.Add(employee);
            }
            else
            {
                var employeeInDb = _context.Employees.Find(employee.Id);

                if (employeeInDb == null)
                {
                    return new JsonResult(NotFound());
                }

                employeeInDb = employee;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(employee));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Employees.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Employees.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
