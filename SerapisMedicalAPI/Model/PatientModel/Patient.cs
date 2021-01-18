using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SerapisMedicalAPI.Enums;
using SerapisMedicalAPI.Model.AppointmentModel;
using SerapisMedicalAPI.Model.MedicalDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.PatientModel
{
    public class Patient
    {
        public ObjectId id { get; set; }
        [BsonElement("socialid")]
        public string SocialID {get; set;}
        [BsonElement("patientfirstname")]
        public string PatientFirstName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("patientlastname")]
        public string PatientLastName { get; set; }
        [BsonElement("patientmedicalaid")]
        public string PatientMedicalAid { get; set; }

        [BsonElement("medicalaidpatient")]
        public bool MedicalAidPatient { get; set; }

        [BsonElement("patientbloodtype")]
        public string PatientBloodType { get; set; }

        [BsonElement("patientage")]
        public int PatientAge { get; set; }

        [BsonElement("hasbloodpressure")]
        public bool HasBloodPressure { get; set; }

        [BsonElement("isdependent")]
        public bool IsDependent { get; set; }

        [BsonElement("listofchronicdisease")]
        public List<ChronicDisease> ListOfChronicDisease { get; set; }

        [BsonElement("listofmedication")]
        public List<Medication> ListOfMedication { get; set; }

        [BsonElement("listofallergies")]
        public List<Allergies> ListOfAllergies { get; set; }

        [BsonElement("medicalrecords")]
        public List<MedicalFile> MedicalRecords { get; set; }

        [BsonElement("patientprofilepicture")]
        private string patientProfilePicture;

        [BsonElement("pastbookedappointments")]
        public List<BookedAppointment> PastBookedAppointments { get; set; }

        [BsonElement("gender")]
        public Genders Gender { get; set; }

        [BsonElement("birthdate")]
        public DateTime BirthDate { get; set; }

        [BsonElement("patientcontactdetails")]
        public PatientContact PatientContactDetails { get; set; }

        [BsonElement("isgoogle")]
        public bool IsGoogle {get; set;}

        [BsonElement("isfacebook")]
        public bool IsFacebook {get; set;}
        
        [BsonElement("token")]
        public string Token { get; set; }

    }
}
