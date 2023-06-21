using Microsoft.Build.Framework;
using SymptomScout.Shared.Models;

namespace SymptomScout.API.Messages.Symptoms
{
    public class UpdateSymptomRequest
    {
        [Required]
        public Symptom Symptom { get; set; }

        public UpdateSymptomRequest() { }
    }
}
