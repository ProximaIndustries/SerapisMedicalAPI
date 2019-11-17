using SerapisMedicalAPI.Core.Dto.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Core.Dto
{
    public class BookedPatient : Appointment
    {
        public Patient Patient { get; set; }
    }
}
