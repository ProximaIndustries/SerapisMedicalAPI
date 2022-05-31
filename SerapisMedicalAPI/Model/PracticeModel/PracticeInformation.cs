using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SerapisMedicalAPI.Model.AppointmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.DoctorModel.Practice
{
    public class PracticeInformation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("practicename")]
        public string PracticeName { get; set; }

        [BsonElement("practicepicture")]
        public string PracticePicture { get; set; }

        [BsonElement("gpscoordinates")]
        public Location GPSCoordinates { get; set; }


        [BsonElement("practiceaddress")]
        public Address PracticeAddress { get; set; }

        [BsonDefaultValue(0.00)]
        [BsonElement("distancefrompractice")]
        public double DistanceFromPractice { get; set; }

        [BsonElement("doctoratpractice")]
        public List<ObjectId> DoctorsAtPractice { get; set; }

        [BsonElement("numofpatientsinpractice")]
        public int NumOfPatientsInPractice { get; set; }

        [BsonElement("operatingtime")]
        public string OperatingTime { get; set; }

        [BsonElement("contactpractice")]
        public PracticeContact ContactPractice { get; set; }

        [BsonElement("appointments")]
        public List<AppointmentDao> Appointment { get; set; }


    }
}
