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
        public IActionResult Create(Employee employee)
        {
            try
            {
                var newEmployee = _employeeService.CreateEmployee(employee);
                return CreatedAtRoute("GetEmployee", new { id = newEmployee.Id }, newEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee employee)
        {
            try
            {
                employee.Id = id;
                var updatedEmployee = _employeeService.UpdateEmployee(employee);
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

        [HttpGet("{id}", Name = "GetEmployee")]
        public IActionResult GetById(int id)
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
        }
    }
}
