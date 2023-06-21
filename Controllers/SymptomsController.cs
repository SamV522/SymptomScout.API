using Microsoft.AspNetCore.Mvc;
using SymptomScout.API.Messages.Symptoms;
using SymptomScout.API.Persistence;

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
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var symptoms = _context.Symptoms.Where(s => s.SymptomId == id);

                if (symptoms != null && symptoms.Any())
                {
                    return Ok(symptoms.Single());
                }
                else
                {
                    return NotFound();
                }
            } catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSymptomAsync([FromBody] CreateSymptomRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _context.Symptoms.Add(request.Symptom);

                await _context.SaveChangesAsync();

                return Ok();
            } catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
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
                symptom.Name = request.Symptom.Name;
                symptom.Description = request.Symptom.Description;
                symptom.DiagnosisSymptoms = request.Symptom.DiagnosisSymptoms;

                await _context.SaveChangesAsync();

                return Ok(symptom);
            } else
            {
                return NotFound();
            }
        }
    }
}
