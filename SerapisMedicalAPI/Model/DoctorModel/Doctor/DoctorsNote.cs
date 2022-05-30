using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.DoctorModel.Doctor
{
    public class DoctorsNote
    {
        
        [BsonElement("noteissuedatetime")]
        public DateTime NoteIssueDateTime{ get; set; }

        [BsonElement("notes")]
        public string Notes { get; set; }
    }
}
