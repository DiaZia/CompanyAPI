using Microsoft.AspNetCore.Mvc;
using CompanyAPI.Services;
using CompanyAPI.Models;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService _companyService;

        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            try
            {
                var newCompany = _companyService.CreateCompany(company);
                return CreatedAtRoute("GetCompany", new { id = newCompany.Id }, newCompany);
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
        public IActionResult Update(int id, Company company) 
        {
            try
            {
                company.Id = id;
                var updatedCompany = _companyService.UpdateCompany(company);
                return Ok(updatedCompany);
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
                _companyService.DeleteCompany(id);
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
