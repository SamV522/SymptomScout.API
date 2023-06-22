using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymptomScout.API.Models.Symptoms;
using SymptomScout.API.Persistence;
using SymptomScout.Shared.Domain;
using SymptomScout.Shared.Models;

namespace SymptomScout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymptomsController : ControllerBase
    {
        private readonly SymptomScoutDbContext _context;

        public SymptomsController(SymptomScoutDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public IActionResult GetSymptoms()
        {
            var symptoms = _context.Symptoms;
            
            return Ok(symptoms);
        }

        [HttpGet("{id}/")]
        public IActionResult GetSymptom(int id)
        {
            var symptom = _context.Symptoms.Single(s => s.SymptomId == id);

            var diagnoses = _context.Diagnoses;

            var symptomDto = new SymptomDto
            {
                SymptomId = symptom.SymptomId,
                Name = symptom.Name,
                Description = symptom.Description,
                Diagnoses = diagnoses.Where(d => d.Symptoms.Select(ds => ds.SymptomId).Single() == symptom.SymptomId).Include(d => d.Symptoms).ToList()
            };

            return Ok(symptomDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSymptomAsync([FromBody] CreateSymptomRequest request)
        {
            var symptom = new Symptom
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Symptoms.Add(symptom);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id}/")]
        public async Task<IActionResult> UpdateSymptomAsync(int id, [FromBody] UpdateSymptomRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var symptom = _context.Symptoms.Where(s => s.SymptomId == id).Single();

            if (symptom != null)
            {
                symptom.Name = request.Name ?? symptom.Name;
                symptom.Description = request.Description ?? symptom.Description;

                await _context.SaveChangesAsync();

                return Ok(symptom);
            } else
            {
                return NotFound();
            }
        }
    }
}
