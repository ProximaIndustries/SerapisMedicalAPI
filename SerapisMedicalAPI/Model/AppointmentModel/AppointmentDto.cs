using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.AppointmentModel
{
    public class AppointmentDto
    {
        //Using int for now, need to use mongoId
        public string BookingId { get; set; }

        public int LineNumber { get; set; }

        public string PatientID { get; set; }

        public DateTime DateAndTimeOfAppointment { get; set; }

        public bool HasSeenGP { get; set; }

        public bool IsSerapisBooking { get; set; }

        public string Duration { get; set; }


        public bool HasBeenToThisPractice { get; set; }

        public string DoctorsId { get; set; }
    
        public string PracticeID { get; set; }
    }
}
