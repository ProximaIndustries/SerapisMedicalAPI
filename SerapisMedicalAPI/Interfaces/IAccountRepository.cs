using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Patient>> GetAllRegisteredUsers();
        Task EditUser(Patient userwithToken);

        Task AddAccount(Patient user);

        Task RegisterSocialUser(Patient patient);

        Task<Patient> SocialLogin(Patient patient);

        //Gets the doctors profile (Both patient and doctor have access to this method) 
        Task<Patient> GetUser(string privateid);

        //Delete-- Doctor from platform (doctor app uses this method)
        

        //Put--Edit doctors informatiion. We must confirm before any changes are made (leave for now)
        Task<bool> UpdateUser(Patient doctor);

        //if doctor needs to edit his information use this method
        //Task<bool> EditUser(Doctor doctor);

        PatientUser RegisterandAuthenticateAsync(Patient user);
    }
}
