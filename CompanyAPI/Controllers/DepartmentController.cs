using CompanyAPI.Data;
using CompanyAPI.Models;
using CompanyAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            try
            {
                var newDepartment = _departmentService.CreateDepartment(department);
                return CreatedAtRoute("GetDepartment", new { id = newDepartment.Id }, newDepartment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Department department)
        {
            try
            {
                department.Id = id;
                var updatedDepartment = _departmentService.UpdateDepartment(department);
                return Ok(updatedDepartment);
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
                _departmentService.DeleteDepartment(id);
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
