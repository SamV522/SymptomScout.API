using SymptomScout.Shared.Domain;

namespace SymptomScout.API.Models.Diagnoses
{
    public class UpdateDiagnosisRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IList<Symptom>? Symptoms { get; set; }

        public UpdateDiagnosisRequest(string name, string description, IList<Symptom> symptoms) 
        {
            Name = name;
            Description = description;
            Symptoms = symptoms;
        }
    }
}
