using System.ComponentModel.DataAnnotations;

namespace SymptomScout.API.Models.Diagnoses
{
    public class RemoveSymptomsRequest
    {
        [Required]
        public IList<int> Symptoms { get; set; }

        public RemoveSymptomsRequest(List<int> symptoms)
        {
            Symptoms = symptoms;
        }
    }
}
