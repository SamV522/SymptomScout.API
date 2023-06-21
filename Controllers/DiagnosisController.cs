using Microsoft.AspNetCore.Mvc;
using SymptomScout.API.Messages.Diagnoses;
using SymptomScout.API.Persistence;

namespace SymptomScout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisController : ControllerBase
    {
        private readonly SymptomScoutDbContext _context;

        public DiagnosisController(SymptomScoutDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDiagnoses()
        {
            var diagnoses = _context.Diagnoses;

            return Ok(diagnoses);
        }

        [HttpGet("{id}/")]
        public IActionResult GetDiagnosis(int id)
        {
            var diagnosis = _context.Diagnoses.Single(d => d.DiagnosisId == id);

            return Ok(diagnosis);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateNewDiagnosis(CreateDiagnosisRequest request)
        {
            _context.Diagnoses.Add(request.Diagnosis);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id}/")]
        public async Task<IActionResult> UpdateDiagnosisAsync(int id, [FromBody] UpdateDiagnosisRequest request)
        {
            var diagnosis = _context.Diagnoses.Single(d => d.DiagnosisId == id);

            diagnosis.Name = request.Diagnosis.Name;
            diagnosis.Description = request.Diagnosis.Description;
            diagnosis.DiagnosisSymptoms = request.Diagnosis.DiagnosisSymptoms;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
