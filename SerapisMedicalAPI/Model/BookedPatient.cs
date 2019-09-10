using SerapisMedicalAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class BookedPatient : Appointment
    {
        public Patient Patient { get; set; }
    }
}
