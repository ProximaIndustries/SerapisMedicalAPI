using SerapisMedicalAPI.Model.DoctorModel.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.AppointmentModel
{
    public class Session
    {
        public TimeSpan Duration { get; set; }
        public DoctorsNote DoctorNote { get; set; }

    }
}
