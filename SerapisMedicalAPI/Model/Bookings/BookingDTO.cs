using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SerapisMedicalAPI.Model.AppointmentModel;
using SerapisMedicalAPI.Model.PatientModel;

namespace SerapisMedicalAPI.Model.Bookings
{
    public class BookingDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        
        public string BookingId { get; set; }
        
        public string PracticeId { get; set; }

        public Patient Patient { get; set; }
        
        public string DoctorsId { get; set; }

        public BookedAppointment BookedAppointment { get; set; }
        
        public DateTime AppointmentDateTime { get; set; }
        
        public bool HasSeenGP { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}
