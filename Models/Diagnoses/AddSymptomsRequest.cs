using Microsoft.Build.Framework;

namespace SymptomScout.API.Models.Diagnoses
{
    public class AddSymptomsRequest
    {
        [Required]
        public IList<int> Symptoms { get; set; }

        public AddSymptomsRequest(List<int> symptoms) 
        {
            Symptoms = new List<int>(symptoms);
        }
    }
}
