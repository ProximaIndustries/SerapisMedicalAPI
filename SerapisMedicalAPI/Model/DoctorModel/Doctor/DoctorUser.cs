using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
namespace SerapisMedicalAPI.Model.DoctorModel.Doctor
{
    public partial class DoctorUser
    {
        [BsonElement("birthdate")]
        public string HealthId { get; set; }

        [BsonElement("birthdate")]
        public string Password { get; set; }
        
        [BsonElement("birthdate")]
        public string Email { get; set; }

        [BsonElement("birthdate")]
        public string GivenCode { get; set; }

        [BsonElement("birthdate")]
        public string LastLogin { get; set; }

        [BsonElement("birthdate")]
        public bool HasbeenValidated { get; set; }
    }
}
