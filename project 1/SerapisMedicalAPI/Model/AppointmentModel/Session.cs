using MongoDB.Bson.Serialization.Attributes;
using SerapisMedicalAPI.Model.DoctorModel.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.AppointmentModel
{
    public class Session
    {

        [BsonElement("duration")]
        public string Duration { get; set; }

        [BsonElement("doctornote")]
        public DoctorsNote DoctorNote { get; set; }

    }
}
