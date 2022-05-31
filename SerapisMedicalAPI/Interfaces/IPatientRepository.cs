using MongoDB.Bson;
using MongoDB.Driver;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerapisMedicalAPI.Data.Base;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IPatientRepository
    {
        //Create-- a new patient to the platform (used for the patient applcation)
        Task AddPatient(Patient newPatientToPlatform);

        Task<BaseResponse<Patient>> GetPatientById(string _id);
        //Read-- Get a patients details
        Task<Patient> GetPatientDetails(ObjectId _id);

        Task<IEnumerable<Patient>> GetAllPatients();

        //Update-- Edit patient information (used both in the doctor app and patient)
        //The doctor uses it to add information and edit incorrect medical information
        //The patient can add more information about themeselves
        Task<BaseResponse<Patient>> EditPatientUser(Patient user);

        //Delete-- the patient from the platfom (used in the patient application)
        Task RemovePatient(Patient _id);
    }
}
