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
        [BsonElement("BookingId")]
        public string BookingId { get; set; }

        [BsonElement("linenumber")]
        public int LineNumber { get; set; }

        [BsonElement("patientid")]
        public string PatientID { get; set; }

        [BsonElement("dateandtimeofappointment")]
        public DateTime DateAndTimeOfAppointment { get; set; }

        [BsonElement("hasseengp")]
        public bool HasSeenGP { get; set; }

        [BsonElement("isserapisbooking")]
        public bool IsSerapisBooking { get; set; }

        [BsonElement("duration")]
        public string Duration { get; set; }

        [BsonElement("hasbeentothispractice")]
        public bool HasBeenToThisPractice { get; set; }

        [BsonElement("doctorsId")]
        public string DoctorsId { get; set; }
        
        [BsonElement("practiceid")] 
        public string PracticeID { get; set; }
    }
}
