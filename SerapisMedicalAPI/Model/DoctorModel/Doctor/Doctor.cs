using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SerapisMedicalAPI.Model.DoctorModel.Doctor;
using SerapisMedicalAPI.Enums;
using SerapisMedicalAPI.Model.DoctorModel.Practice;

namespace SerapisMedicalAPI.Model
{
    public class Doctor
    {
        public DoctorUser User { get; set; }

        public string PrivateId { get; set; }

        public Genders Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int YearsOfExp { get; set; }

        public string ProfilePicture { get; set; }

        public List<Qualification> Qualifications { get; set; }

        public Specilization Specialization { get; set; }

        public DoctorsNote Note { get; set; }

        public DoctorPrescription Prescription { get; set; }

        public List<PracticeInformation> PracticesOwnedOrWorksAt { get; set; }

        public List<AppointmentModel.Appointment> ListOfAppointments { get; set; }
    }
}
