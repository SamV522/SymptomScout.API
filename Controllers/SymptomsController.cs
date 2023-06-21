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

        public SymptomsController(SymptomScoutDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet()]
        public IActionResult GetAllSymptoms()
        {
            var symptoms = _context.Symptoms;

            return Ok(symptoms);
        }

        [HttpGet("{id}/")]
        public IActionResult GetSymptomById(int id)
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
        public async Task<IActionResult> CreateNewSymptom(NewSymptomRequest request)
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
    }
}
