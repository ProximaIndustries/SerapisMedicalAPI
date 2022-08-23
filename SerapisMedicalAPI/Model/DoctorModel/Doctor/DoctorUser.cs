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
        [BsonElement("healthid")]
        public string HealthId { get; set; }
        
        [BsonElement("authId")]
        public string AuthId { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
        
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("givencode")]
        public string GivenCode { get; set; }

        [BsonElement("lastlogin")]
        public DateTime LastLogin { get; set; }

        [BsonElement("hasbeenvalidated")]
        public bool HasbeenValidated { get; set; }
        
        [BsonElement("phone")]
        public string Phone { get; set; }
    }
}
