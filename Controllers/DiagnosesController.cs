using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SymptomScout.API.Models.Diagnoses;
using SymptomScout.API.Persistence;
using SymptomScout.Shared.Domain;
using System.Collections.Generic;

namespace SymptomScout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosesController : ControllerBase
    {
        private readonly SymptomScoutDbContext _context;

        public DiagnosesController(SymptomScoutDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDiagnoses()
        {
            var diagnoses = _context.Diagnoses;

            return Ok(diagnoses.Include(d => d.Symptoms));
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
            var diagnosis = new Diagnosis
            {
                Name = request.Name,
                Description = request.Description,
                Symptoms = request.Symptoms
            };

            _context.Diagnoses.Add(diagnosis);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id}/")]
        public async Task<IActionResult> UpdateDiagnosisAsync(int id, [FromBody] UpdateDiagnosisRequest request)
        {
            var diagnosis = _context.Diagnoses.Single(d => d.DiagnosisId == id);

            diagnosis.Name = request.Name ?? diagnosis.Name;
            diagnosis.Description = request.Description ?? diagnosis.Description;
            diagnosis.Symptoms = request.Symptoms ?? diagnosis.Symptoms ?? new List<Symptom>();

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id}/symptoms/add/{symptomId}")]
        public async Task<IActionResult> AddSymptomAsync(int id, int symptomId)
        {
            // Validate Symptom Id
            var symptom = _context.Symptoms.Single(s => s.SymptomId == symptomId);

            // Validate Diagnosis Id
            var diagnosis = _context.Diagnoses.Single(d => d.DiagnosisId == id);

            var diagnosisSymptoms = diagnosis.Symptoms;

            if (diagnosisSymptoms != null)
                diagnosisSymptoms.Add(symptom);
            else
                diagnosis.Symptoms = new List<Symptom>() { symptom };

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id}/symptoms/addrange")]
        public async Task<IActionResult> AddSymptomsAsync(int id, AddSymptomsRequest request)
        {
            // Validate Symptom Id
            var symptoms = _context.Symptoms.Where(s => request.Symptoms.Contains(s.SymptomId)).ToList();

            // Validate Diagnosis Id
            var diagnosis = _context.Diagnoses.Single(d => d.DiagnosisId == id);

            foreach(var symptom in symptoms)
            {
                diagnosis.Symptoms.Add(symptom);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id}/symptoms/remove/{symptomId}")]
        public async Task<IActionResult> RemoveSymptomAsync(int id, int symptomId)
        {
            // Validate Symptom Id
            var symptom = _context.Symptoms.Single(s => s.SymptomId == symptomId);

            // Validate Diagnosis Id
            var diagnosis = _context.Diagnoses.Single(d => d.DiagnosisId == id);

            diagnosis.Symptoms.Remove(symptom);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id}/symptoms/removerange")]
        public async Task<IActionResult> RemoveSymptomsAsync(int id, RemoveSymptomsRequest request)
        {
            // Validate Symptom Ids
            var symptoms = _context.Symptoms.Where(s => request.Symptoms.Contains(s.SymptomId)).ToList();

            // Validate Diagnosis Id
            var diagnosis = _context.Diagnoses.Single(d => d.DiagnosisId == id);

            foreach (var symptom in symptoms)
            {
                diagnosis.Symptoms.Remove(symptom);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
