using SymptomScout.Shared.Domain;
using System.ComponentModel.DataAnnotations;

namespace SymptomScout.API.Models.Symptoms
{
    public class CreateSymptomRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IList<Diagnosis> Diagnoses { get; set; }

        public CreateSymptomRequest(string name, string description, IList<Diagnosis> diagnoses) 
        {
            Name = name;
            Description = description;
            Diagnoses = diagnoses;
        }
    }
}
