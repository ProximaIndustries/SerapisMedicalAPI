using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SerapisMedicalAPI.Model.DoctorModel.Doctor;
using SerapisMedicalAPI.Enums;
using SerapisMedicalAPI.Model.DoctorModel.Practice;

namespace SerapisMedicalAPI.Model
{
    public class Doctor
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("privateId")]
        public string PrivateId { get; set; }

        [BsonElement("firstname")]
        public string FirstName { get; set; }
        
        [BsonElement("lastname")]
        public string LastName { get; set; }

        [DefaultValue("Other")]
        [BsonElement("gender")]
        public Genders Gender { get; set; }

        [BsonElement("birthdate")]
        public DateTime BirthDate { get; set; }

        //[DefaultValue(10)]
        //[BsonElement("yearsofexp")]
        //public int YearsOfExp { get; set; }

        [BsonElement("profileImageUrl")]
        public string ProfilePicture { get; set; }

        [BsonElement("qualifications")]
        public List<Qualification> Qualifications { get; set; }

        [BsonElement("specialization")]
        public Specilization Specialization { get; set; }

        //[BsonElement("note")]
        //public DoctorsNote Note { get; set; }

        //[BsonElement("Prescription")]
        //public DoctorPrescription Prescription { get; set; }

        [BsonElement("practicesownedOrworksat")]
        //public List<PracticeInformation> PracticesOwnedOrWorksAt { get; set; }
        public List<ObjectId> PracticesOwnedOrWorksAt { get; set; }

        [BsonElement("listofappointments")]
        //public List<AppointmentModel.Appointment> ListOfAppointments { get; set; }
        public List<ObjectId> ListOfAppointments { get; set; }

        [BsonElement("user")]
        public DoctorUser User { get; set; }
    }
}
