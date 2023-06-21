using Microsoft.Build.Framework;
using SymptomScout.Shared.Models;

namespace SymptomScout.API.Messages.Diagnoses
{
    public class CreateDiagnosisRequest
    {
        [Required]
        public Diagnosis Diagnosis { get; set; }

        public CreateDiagnosisRequest() { }
    }
}
