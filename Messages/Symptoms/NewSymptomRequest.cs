using SymptomScout.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace SymptomScout.API.Messages.Symptoms
{
    public class NewSymptomRequest
    {
        [Required]
        public Symptom Symptom { get; set; }

        public NewSymptomRequest() { }
    }
}
