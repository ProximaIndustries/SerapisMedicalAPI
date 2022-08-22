using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerapisMedicalAPI.Data.Base;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Patient>> GetAllRegisteredUsers();
        Task EditUser(Patient userwithToken);

        BaseResponse<Patient> AddPatientAccount(Patient user);
        Task<BaseResponse<Patient>> AddAccountAsync(Patient user);

        Task RegisterSocialUser(Patient patient);

        Task<Patient> SocialLogin(string socialid, string firstname, string lastname);

        //Gets the doctors profile (Both patient and doctor have access to this method) 
        Task<BaseResponse<Doctor>> GetPractionerById(string privateid);
        Task<BaseResponse<Patient>> GetUserById(string privateid);

        //Delete-- Doctor from platform (doctor app uses this method)
        
        //Put--Edit doctors informatiion. We must confirm before any changes are made (leave for now)
        Task<bool> UpdateUser(Patient doctor);

        //if doctor needs to edit his information use this method
        //Task<bool> EditUser(Doctor doctor);

        PatientUser RegisterandAuthenticateAsync(Patient user);
    }
}
