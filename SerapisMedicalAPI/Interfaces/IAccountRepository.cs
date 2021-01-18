using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<PatientUser>> GetAllRegisteredUsers();
        Task EditUser(PatientUser userwithToken);

        Task AddAccount(PatientUser user);

        Task RegisterSocialUser(PatientUser patient);

        Task<PatientUser> FBLogin(string socialID, string FirstName,string LastName, string emailaddress);

        Task<PatientUser> FacebookLogin(PatientUser patient);

        //Gets the doctors profile (Both patient and doctor have access to this method) 
        Task<PatientUser> GetUser(string privateid);

        //Delete-- Doctor from platform (doctor app uses this method)
        

        //Put--Edit doctors informatiion. We must confirm before any changes are made (leave for now)
        Task<bool> UpdateUser(PatientUser doctor);

        //if doctor needs to edit his information use this method
        //Task<bool> EditUser(Doctor doctor);

        PatientUser RegisterandAuthenticateAsync(PatientUser user);
    }
}
