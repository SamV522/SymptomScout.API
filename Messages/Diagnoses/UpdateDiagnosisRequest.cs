using Microsoft.Build.Framework;
using SymptomScout.Shared.Models;

namespace SymptomScout.API.Messages.Diagnoses
{
    public class UpdateDiagnosisRequest
    {
        [Required]
        public Diagnosis Diagnosis { get; set; }

        public UpdateDiagnosisRequest() { }
    }
}
