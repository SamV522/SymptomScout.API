using SymptomScout.Shared.Domain;
using SymptomScout.Shared.Models;

namespace SymptomScout.API.Models.Diagnoses
{
    public class CreateDiagnosisRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<SymptomDto> Symptoms { get; set; }

        public static explicit operator Diagnosis(CreateDiagnosisRequest createDiagnosisRequest)
        {
            return new Diagnosis()
            {
                Name = createDiagnosisRequest.Name,
                Description = createDiagnosisRequest.Description,
                Symptoms = createDiagnosisRequest.Symptoms.Select(s => new Symptom()
                {
                    Name = s.Name,
                    Description = s.Description,
                    Diagnoses = new List<Diagnosis>()
                }).ToList()
            };
        }
    }
}
