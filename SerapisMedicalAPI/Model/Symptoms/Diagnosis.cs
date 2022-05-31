using System.Collections.Generic;

namespace SerapisMedicalAPI.Model.Symptoms
{
    public class Diagnosis
    {
        public Issue Issue { get; set; }
        public List<Specialisation> Specialisation { get; set; }
        
        
    }
}