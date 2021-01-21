using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.MedicalDetails
{
    public class MedicalFile
    {
        [BsonElement("medicalfileid")]
        public string MedicalFileId { get; set; }

        [BsonElement("file")]
        public string File { get; set; }

        [BsonElement("dateandtimecreated")]
        public DateTime DateAndTimeCreated { get; set; }

        [BsonElement("typeoffile")]
        public string TypeOfFile { get; set; }
    }
}
