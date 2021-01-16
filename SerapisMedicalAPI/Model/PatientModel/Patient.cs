using MongoDB.Bson;
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

        public string SocialID {get; set;}
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientMedicalAid { get; set; }
        public bool MedicalAidPatient { get; set; }
        public string PatientBloodType { get; set; }
        public int PatientAge { get; set; }
        public bool HasBloodPressure { get; set; }
        public bool IsDepenedent { get; set; }
        public List<ChronicDisease> ListOfChronicDisease { get; set; }
        public List<Medication> ListOfMedication { get; set; }
        public List<Allergies> ListOfAllergies { get; set; }
        public List<MedicalFile> MedicalRecords { get; set; }

        private string patientProfilePicture;

        public string GetPatientProfilePicture()
        {
            return patientProfilePicture;
        }

        public void SetPatientProfilePicture(string value)
        {
            patientProfilePicture = value;
        }

        public List<BookedAppointment> PastBookedAppointments { get; set; }
        public string PatientProfilePicture { get; set; }

        public Genders Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public PatientContact PatientContactDetails { get; set; }

        public bool IsGoogle {get; set;}
        public bool IsFacebook {get; set;}

    }
}
