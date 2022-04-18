using System.Collections.Generic;
using SerapisMedicalAPI.Model.Symptoms;

namespace SerapisMedicalAPI.Interfaces
{
    public interface ISymptomCheckerRepository
    {

        Symptoms GetSymptomById();
        
        /// <summary>
        /// Service to get billers by bill type id
        /// </summary>
        /// <param name="billTypeId"></param>
        /// <returns></returns>
        void PopulateSymptoms(IEnumerable<Symptoms> symptomsEnumerable);

        IEnumerable<DiagnosisResponse> GetProposedDiagnosisBySymptoms(string id, string age,string sex);
    }
}