using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SymptomScout.API.Persistence;
using SymptomScout.Shared.Domain;
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

        [HttpPost("symptoms/match")]
        public IActionResult DiagnosisSymptomSearch([FromBody] List<int> symptomIds)
        {
            // Validate all symptoms
            var symptoms = _context.Symptoms.Where(s => symptomIds.Contains(s.SymptomId));

            var diagnoses = _context.Diagnoses.AsQueryable();

            foreach(var symptomId in symptomIds)
            {
                diagnoses = diagnoses.Where(d => d.Symptoms.Select(ds => ds.SymptomId).Contains(symptomId));
            }

            return Ok(diagnoses.Include(d => d.Symptoms));
        }
    }
}
