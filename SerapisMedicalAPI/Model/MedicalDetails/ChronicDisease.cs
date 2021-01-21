using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

using MongoDB.Bson.Serialization.Attributes;

namespace SerapisMedicalAPI.Model.MedicalDetails
{
    public class ChronicDisease
    {
        [BsonElement("chronicid")]
        public string chronicId { get; set; }
        [BsonElement("chronicdiseasename")]
        public string ChronicDiseaseName { get; set; }
    }
}
