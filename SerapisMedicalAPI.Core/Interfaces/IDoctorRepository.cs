using MongoDB.Bson;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SerapisMedicalAPI.Interfaces
{
    public interface IDoctorRepository
    {
        //Add doctor to the platform (Doctor uses this method)
         Task AddDoctor(Doctor Doc);

        //Gets all avalaible doctors --Used with patient app
        Task<IEnumerable<Doctor>> GetAllDoctor();

        //Gets the doctors profile (Both patient and doctor have access to this method) 
        Task<Doctor> GetDoctor(string privateid);

        //Delete-- Doctor from platform (doctor app uses this method)
        Task<Doctor> EditDoctor(ObjectId _id);

        //Put--Edit doctors informatiion. We must confirm before any changes are made (leave for now)

        //if doctor needs to edit his information use this method
        Task<bool> EditDoctor(Doctor doctor);
    }
}
