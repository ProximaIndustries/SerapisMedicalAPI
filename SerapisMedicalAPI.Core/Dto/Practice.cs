using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SerapisMedicalAPI.Core.Dto
{
    public class Practice
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("practiceName")]
        public string PracticeName { get; set; }

        [BsonElement("practicePicture")]
        public string PracticePicture { get; set; }

        [BsonElement("location")]
        public Location Location { get; set; }

        [BsonElement("DoctorAvaliable")]
        public List<Doc> DoctorAvaliable { get; set; }

        [BsonElement("appointments")]
        public List<Appointment> Appointments { get; set; }
    }
    public partial class Doc
    {
        [BsonRepresentation(BsonType.ObjectId)]
        //[BsonId]
        public ObjectId docid { get; set; }
    }
    public partial class Location
    {
        [BsonElement("latitude")]
        public string Latitude { get; set; }

        [BsonElement("longitude")]
        public string Longitude { get; set; }

        [BsonElement("workAddress")]
        public WorkAddress WorkAddress { get; set; }

    }
    public partial class Appointment
    {
        // most attributes will take on the objectid formatted as a string
        [BsonId]
        public ObjectId appointmentId { get; set; }

        [BsonElement("patientId")]
        public ObjectId Patient { get; set; }

        [BsonElement("dateAndTime")]
        public DateTime DateandTime { get; set; }

        [BsonElement("venueId")]
        public ObjectId Venue { get; set; }

        [BsonElement("doctorBooked")]
        public string DoctorBooked { get; set; }

        [BsonElement("isSerapisBooking")]
        public bool IsSerapisBooking { get; set; }

        [BsonElement("hasSeenGp")]
        public bool HasSeenGp { get; set; }

        [BsonElement("dateBooked")]
        public string DateBooked { get; set; }

        [BsonElement("timeBooked")]
        public string TimeBooked { get; set; }
    }

    public partial class WorkAddress
    {

        [BsonElement("addressLine1")]
        public string AddressLine1 { get; set; }

        [BsonElement("addressLine2")]
        public string AddressLine2 { get; set; }

        [BsonElement("townOrCity")]
        public string TownOrCity { get; set; }

        [BsonElement("postal")]
        public int Postal { get; set; } //might be a long type !=int

    }
}
