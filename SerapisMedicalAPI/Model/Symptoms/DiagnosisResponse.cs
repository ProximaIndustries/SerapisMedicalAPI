using System.Collections.Generic;

namespace SerapisMedicalAPI.Model.Symptoms
{
    public class DiagnosisResponse
    {
        //public List<Diagnosis> Diagnoses { get; set; }
        public Issue Issue { get; set; }
        public List<Specialisation> Specialisation { get; set; }
    }
}