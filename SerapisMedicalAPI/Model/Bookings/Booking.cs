using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SerapisMedicalAPI.Model.AppointmentModel;

namespace SerapisMedicalAPI.Model.Bookings
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id  { get; set; }
        
        [BsonElement("bookingid")]
        public string BookingId { get; private set; }
        
        [BsonElement("practiceid")]
        public string PracticeId { get; private set; }
        
        [BsonElement("doctorsid")]
        public string DoctorsId { get; private set; }
        
        [BsonElement("bookedappointment")]
        public BookedAppointment BookedAppointment { get; private set; }
        
        [BsonElement("appointmentdatetime")]
        public DateTime AppointmentDateTime { get; private set; }
        
        [BsonElement("hasseengp")]
        public bool HasSeenGP { get; set; }
        
        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        public Booking(string id,string BookingId, string PracticeId, string DoctorsId,
            BookedAppointment BookedAppointment, DateTime AppointmentDateTime, bool HasSeenGP  )
        {
            this.id = id;
            this.BookingId = BookingId;
            this.PracticeId = PracticeId;
            this.DoctorsId = DoctorsId;
            this.BookedAppointment = BookedAppointment;
            this.CreatedDate = DateTime.Now;
            this.AppointmentDateTime = AppointmentDateTime;
            this.HasSeenGP = HasSeenGP;
        }
    }
}