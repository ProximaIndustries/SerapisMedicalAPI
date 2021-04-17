using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.AppointmentModel
{
    public class Appointmentv2
    {

        //Using int for now, need to use mongoId
        public ObjectId BookingId { get; set; }


        [BsonElement("linenumber")]
        public int LineNumber { get; set; }

        [BsonElement("patientid")]
        public ObjectId PatientID { get; set; }

        [BsonElement("dateandtimeofappointment")]
        public DateTime DateAndTimeOfAppointment { get; set; }

        [BsonElement("hasseengp")]
        public bool HasSeenGP { get; set; }

        [BsonElement("isserapisbooking")]
        public bool IsSerapisBooking { get; set; }

        [BsonElement("duration")]
        public string Duration { get; set; }

        // BN: MONGODB DOESNT SUPPORT TIMESPAN
        //public TimeSpan Duration { get; set; }

        [BsonElement("hasbeentothispractice")]
        public bool HasBeenToThisPractice { get; set; }

        [BsonElement("doctorsId")]
        public ObjectId DoctorsId { get; set; }

        //    [BsonElement("venue")]
        //    public Address Venue { get; set; }
        [BsonElement("practiceid")]
        public ObjectId PracticeID { get; set; }
    }
}
