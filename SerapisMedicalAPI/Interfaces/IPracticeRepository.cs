using MongoDB.Bson;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI
{
    public interface IPracticeRepository
    {
        //Get all the practices (Used in the patient application)
        //max distance is the radius from whicih to pick from (user settings)
        //using geospatial in mongo to query and leave out practices that are out of the radius
        Task<IEnumerable<Practice>> GetPractices();

        Task<IEnumerable<Practice>> GetPractices(object _id, double maxDistance);

        Task<PracticeInformation> GetPracticeById(ObjectId _id);
        //Get practice details (used in the patient application)
        Task<Practice> GetPracticeDetails(object _id);

        //Remove practice (used in the doctors application)
        Task<Practice> RemovePractice(object _id);

        //Put edit practice information (used in the doctors application)
        Task<Practice> EditPracticeInfo(object _id);

        //Create a practice --needs verification first
        //So I am going to leave it for now 
        Task AddPractice(Practice practice);
    }
}
