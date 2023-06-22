using Microsoft.Build.Framework;
using SymptomScout.Shared.Domain;

namespace SymptomScout.API.Models.Diagnoses
{
    public class CreateDiagnosisRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Symptom> Symptoms { get; set; }
    }
}
