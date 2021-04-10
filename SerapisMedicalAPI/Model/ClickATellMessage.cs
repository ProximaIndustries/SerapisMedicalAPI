using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class ClickATellMessage
    {
        [BsonElement("channel")]
        public string channel { get; set; }

        [BsonElement("to")]
        public string To { get; set; }

        [BsonElement("content")]
        public string Content { get; set; } 
    }
}
