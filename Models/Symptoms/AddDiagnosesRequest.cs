using NuGet.Packaging;
using System.ComponentModel.DataAnnotations;

namespace SymptomScout.API.Models.Symptoms
{
    public class AddDiagnosesRequest
    {
        [Required]
        public IList<int> Diagnoses { get; set; }

        public AddDiagnosesRequest(List<int> diagnoses)
        {
            Diagnoses = new List<int>(diagnoses);
        }
    }
}
