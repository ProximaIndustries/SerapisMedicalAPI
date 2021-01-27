using MongoDB.Bson.Serialization.Attributes;
using SerapisMedicalAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.DoctorModel.Doctor
{
    public class Qualification
    {
        [BsonElement("degree")]
        public string Degree { get; set; }

        [BsonElement("graduated")]
        public DateTime Graduated { get; set; }

        [BsonElement("university")]
        public string University { get; set; }

        [BsonElement("abbr")]
        public string Abbr { get; set; }

        [BsonElement("specilization")]
        public Specilization Specilization { get; set; }

        [BsonElement("specilizationabbr")]
        public string Specilizationabbr { get; set; }
    }
}
