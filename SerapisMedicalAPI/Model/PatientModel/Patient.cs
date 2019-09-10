using SerapisMedicalAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.PatientModel
{
    public class Patient
    {
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientMedicalAid { get; set; }
        public bool MedicalAidPatient { get; set; }
        public string PatientBloodType { get; set; }
        public int PatientAge { get; set; }
        public bool HasBloodPressure { get; set; }
        public bool IsDepenedent { get; set; }
        public List<string> ListOfChronicDisease { get; set; }
        public List<string> ListOfMedication { get; set; }
        public List<string> ListOfAllergies { get; set; }
        public List<string> MedicalHistoryRecord { get; set; }
        public string PatientProfilePicture { get; set; }
        public Genders Gender { get; set; }
    }
}
