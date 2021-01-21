using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.MedicalDetails
{
    public class Allergies
    {
        [BsonElement("allergiesid")]
        public string AllergiesId { get; set; }

        [BsonElement("allergiesname")]
        public string AllergyName { get; set; }
    }
}
