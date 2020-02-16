using MongoDB.Bson;
using SerapisMedicalAPI.Model.AppointmentModel;

namespace SerapisMedicalAPI.Model.PatientModel
{
    public class PatientDto
    {
        public ObjectId PatientId { get; set; }

        public string ProfilePicture { get; set; }

        public bool MedicalAidPatient { get; set; }

        public AppointmentDto AppointmentSet { get; set; }

        public string FullName { get; set; }
    }
}
