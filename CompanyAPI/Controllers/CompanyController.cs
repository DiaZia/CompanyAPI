using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyAPI.Models;
using CompanyAPI.Data;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ApiContext _context;

        public CompanyController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(Company company)
        {
            if (company.Id == 0)
            {
                _context.Companies.Add(company);
            }
            else
            {
                var companyInDb = _context.Companies.Find(company.Id);

                if (companyInDb == null)
                {
                    return new JsonResult(NotFound());
                }

                companyInDb = company;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(company));
        }

        [HttpDelete] 
        public JsonResult Delete(int id)
        {
            var result = _context.Companies.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Companies.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}