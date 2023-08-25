using CompanyAPI.Data;
using CompanyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private readonly ApiContext _context;

        public DivisionController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(Division division)
        {
            if (division.Id == 0)
            {
                _context.Divisions.Add(division);
            }
            else
            {
                var divisionInDb = _context.Divisions.Find(division.Id);

                if (divisionInDb == null)
                {
                    return new JsonResult(NotFound());
                }

                divisionInDb = division;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(division));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Divisions.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Divisions.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
