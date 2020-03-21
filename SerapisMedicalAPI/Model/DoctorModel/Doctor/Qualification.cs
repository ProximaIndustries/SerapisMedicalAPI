using SerapisMedicalAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.DoctorModel.Doctor
{
    public class Qualification
    {
        public string Degree { get; set; }


        public DateTime Graduated { get; set; }


        public string University { get; set; }


        public string Abbr { get; set; }


        public Specilization Specilization { get; set; }


        public string Specilizationabbr { get; set; }
    }
}
