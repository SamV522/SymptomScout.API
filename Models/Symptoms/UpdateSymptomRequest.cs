using Microsoft.Build.Framework;
using SymptomScout.Shared.Domain;

namespace SymptomScout.API.Models.Symptoms
{
    public class UpdateSymptomRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IList<Symptom>? Symptoms { get; set; }

        public UpdateSymptomRequest(string name, string description, IList<Symptom> symptoms)
        {
            Name = name;
            Description = description;
            Symptoms = symptoms;
        }
    }
}
