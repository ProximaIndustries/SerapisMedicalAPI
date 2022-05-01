using System.Collections.Generic;
using System.Threading.Tasks;
using SerapisMedicalAPI.Model.Symptoms;

namespace SerapisMedicalAPI.Interfaces
{
    public interface ISymptomCheckerRepository
    {

        Task<IEnumerable<Symptoms>> GetAllSymptoms();
        Symptoms GetSymptomById();
        
        Task PopulateSymptoms(IEnumerable<Symptoms> symptomsEnumerable);

        IEnumerable<DiagnosisResponse> GetProposedDiagnosisBySymptoms(string id, string age,string sex);
    }
}