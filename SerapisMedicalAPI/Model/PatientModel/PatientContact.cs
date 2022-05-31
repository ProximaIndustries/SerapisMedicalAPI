using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace SerapisMedicalAPI.Model.PatientModel
{
    public class PatientContact
    {
        [BsonElement("cellphonenumber")]
        public string CellphoneNumber { get; set; }
        
        [BsonElement("email")]
        public string Email { get; set; }
        
        [BsonElement("telephone")]
        public string Telephone { get; set; }
    }
}
