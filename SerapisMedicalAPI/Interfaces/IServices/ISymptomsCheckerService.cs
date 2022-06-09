using SerapisMedicalAPI.Model.Symptoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Services.SymptomsChecker
{
    public interface ISymptomsCheckerService
    {
        SymptomToken GetToken();
        IEnumerable<Symptoms> GetInstanceAllSymptoms();
        Task<IEnumerable<Symptoms>> GetAllSymptoms();
        Task GetSymptomsBySublocations();
        Task<IEnumerable<DiagnosisResponse>> GetProposedDiagnosisBySymptoms(string gender ,string age,int[] ids);
        Task GetSepecialistionsBasedOnDiagnosis();
    }
}
