using Microsoft.AspNetCore.Mvc;
using SymptomScout.API.Persistence;
using System.ComponentModel.DataAnnotations;

namespace SymptomScout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SymptomScoutDbContext _context;

        public SearchController(SymptomScoutDbContext context) 
        {
            _context = context;
        }

        [HttpGet("symptoms/{search}")]
        public IActionResult SymptomSearch([MinLength(3)] string search)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var symptom = _context.Symptoms.Where(s => s.Name.Substring(0,search.Length) == search);

            if (symptom.Any())
                return Ok(symptom);
            else
                return NotFound();
        }

        [HttpGet("diagnosis/{search}")]
        public IActionResult DiagnosisSearch([MinLength(3)] string search)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diagnosis = _context.Diagnoses.Where(x => x.Name.Substring(0, search.Length) == search);

            if (diagnosis.Any())
                return Ok(diagnosis);
            else
                return NotFound();
        }
    }
}
