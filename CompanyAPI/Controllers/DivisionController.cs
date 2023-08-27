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
        public IActionResult CreateEdit(Division division)
        {
            try
            {
                var newDivision = _divisionService.CreateDivision(division);
                return CreatedAtRoute("GetDivision", new { id = newDivision.Id }, newDivision);
                
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
        public IActionResult Update(int id, Division division)
        {
            try
            {
                division.Id = id;
                var updatedDivision = _divisionService.UpdateDivision(division);
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
