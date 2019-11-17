using MongoDB.Bson;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IPatientRepository
    {
        //Create-- a new patient to the platform (used for the patient applcation)
        Task AddPatient(PatientUser newPatientToPlatform);

        //Read-- Get a patients details
        Task<PatientUser> GetPatientDetails(ObjectId _id);

        Task<IEnumerable<PatientUser>> GetAllPatients();
        
        //Update-- Edit patient information (used both in the doctor app and patient)
        //The doctor uses it to add information and edit incorrect medical information
        //The patient can add more information about themeselves
        Task EditPatientUser(PatientUser _id);

        //Delete-- the patient from the platfom (used in the patient application)
        Task RemovePatient(PatientUser _id);
    }
}
