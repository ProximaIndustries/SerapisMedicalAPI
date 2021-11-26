using SerapisMedicalAPI.Model.Symptoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Services.SymptomsChecker
{
    public interface ISymptomsCheckerService
    {
        IEnumerable<Symptoms> GetInstanceAllSymptoms();
        IEnumerable<Symptoms> GetAllSymptoms();
        Task GetSymptomsBySublocations();
        Task GetProposedDiagnosisBySymptoms();
        Task GetSepecialistionsBasedOnDiagnosis();
    }
}
