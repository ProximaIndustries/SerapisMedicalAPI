using System.Collections.Generic;
using System.Threading.Tasks;
using SerapisMedicalAPI.Model.Symptoms;

namespace SerapisMedicalAPI.Services.SymptomsChecker
{
    public class Apimedic :  ISymptomsCheckerService
    {
        public IEnumerable<Symptoms> GetInstanceAllSymptoms()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Symptoms> GetAllSymptoms()
        {
            throw new System.NotImplementedException();
        }

        public Task GetSymptomsBySublocations()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DiagnosisResponse> GetProposedDiagnosisBySymptoms(string gender, string age, int[] ids)
        {
            throw new System.NotImplementedException();
        }

        public Task GetSepecialistionsBasedOnDiagnosis()
        {
            throw new System.NotImplementedException();
        }
    }
}