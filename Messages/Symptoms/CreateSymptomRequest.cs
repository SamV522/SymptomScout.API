using SymptomScout.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace SymptomScout.API.Messages.Symptoms
{
    public class CreateSymptomRequest
    {
        [Required]
        public Symptom Symptom { get; set; }

        public CreateSymptomRequest() { }
    }
}
