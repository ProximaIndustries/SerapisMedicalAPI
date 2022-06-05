using MongoDB.Bson;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using SerapisMedicalAPI.Model.PracticeModel;
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
        Task<IEnumerable<PracticeInformation>> GetAllPractices();
        //Task<IEnumerable<PracticeInformation>> GetPractices();

        //Get the practice dtos
        Task<IEnumerable<PracticeDto>> GetPractices(double lat, double lon, double maxDistance);

        Task<PracticeInformation> GetPracticeIfDoctorWorksThere(string _id);

        Task<PracticeInformation> GetPracticeById(string _id);
        //Get practice details (used in the patient application)
        Task<PracticeInformation> GetPracticeDetails(string _id);

        //Remove practice (used in the doctors application)
        Task<PracticeInformation> RemovePractice(string _id);

        //Put edit practice information (used in the doctors application)
        Task<PracticeInformation> EditPracticeInfo(string _id);

        //Create a practice --needs verification first
        //So I am going to leave it for now 
        Task AddPractice(PracticeInformation practice);
    }
}
