using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class PracticeContact
    {
        [BsonElement("twitterhandle")]
        public string TwitterHandle { get; set; }
        [BsonElement("facebookhandle")]
        public string FacebookHandle { get; set; }
        [BsonElement("practicetelephonenumber")]
        public string PracticeTelephoneNumber { get; set; }
        [BsonElement("practiceemail")]
        public string PracticeEmail { get; set; }
        [BsonElement("faxnumber")]
        public string FaxNumber { get; set; }
        [BsonElement("website")]
        public string Website { get; set; }
    }
}
