namespace SymptomScout.API.Models.Symptoms
{
    public class RemoveDiagnosesRequest
    {
        public IList<int> Diagnoses { get; set; }

        public RemoveDiagnosesRequest(List<int> diagnoses)
        {
            Diagnoses = new List<int>(diagnoses);
        }
    }
}
