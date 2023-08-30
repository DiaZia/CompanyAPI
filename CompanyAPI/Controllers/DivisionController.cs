using CompanyAPI.Models;
using CompanyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private readonly DivisionService _divisionService;

        public DivisionController(DivisionService divisionService)
        {
            _divisionService = divisionService;
        }

        [HttpPost]
        public IActionResult Create(string code, string name, int? leaderId, int companyId)
        {
            Division division = new Division(code, name, leaderId, companyId);  
            try
            {
                var newDivision = _divisionService.CreateDivision(division);
                return Ok(newDivision);
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

        [HttpGet("{id}")]
        public IActionResult GetDivisionById(int id)
        {
            try
            {
                var division = _divisionService.GetDivisionById(id);
                return Ok(division);
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
        public IActionResult Update(int id, string? code, string? name, int? leaderId, int? companyId)
        {
            try
            {
                var updatedDivision = _divisionService.UpdateDivision(id, code, name, leaderId, companyId);
                return Ok(updatedDivision);
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
                _divisionService.DeleteDivision(id);
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
