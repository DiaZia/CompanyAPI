using CompanyAPI.Services;
using CompanyAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
         private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult Create(string? title, string firstName, string lastName, string? phone, string? email)
        {
            Employee employee = new Employee(title, firstName, lastName, phone, email);
            try
            {
                var newEmployee = _employeeService.CreateEmployee(employee);
                return Ok(newEmployee);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(id);
                return Ok(employee);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, string? title, string? firstName, string? lastName, string? phone, string? email)
        {
            try
            {
                var updatedEmployee = _employeeService.UpdateEmployee(id, title, firstName, lastName, phone, email);
                return Ok(updatedEmployee);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        
    }
}
