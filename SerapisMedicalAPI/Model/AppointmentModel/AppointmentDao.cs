using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.AppointmentModel
{
    public class AppointmentDao
    {
        //Using int for now, need to use mongoId
        /*[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]*/
        public string BookingId { get; set; }


        [BsonElement("linenumber")]
        public int LineNumber { get; set; }

        /*[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]*/
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

        // BN: MONGODB DOESNT SUPPORT TIMESPAN
        //public TimeSpan Duration { get; set; }

        [BsonElement("hasbeentothispractice")]
        public bool HasBeenToThisPractice { get; set; }
        
        /*[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]*/
        [BsonElement("doctorsId")]
        public string DoctorsId { get; set; }

        /*[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]*/
        [BsonElement("practiceid")]
        public string PracticeID { get; set; }
    }
}
