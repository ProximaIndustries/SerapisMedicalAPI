using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class Messaging
    {
        [BsonElement("messages")]
        public List<ClickATellMessage> Messages { get; set; }
    }
}
