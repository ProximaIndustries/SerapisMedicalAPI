﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SerapisMedicalAPI.Model
{
    public class Doctor
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string PrivateId { get; set; }
        //[BsonRepresentation(BsonType.ObjectId)]
        //public String  { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("profileImageUrl")]
        public string ProfileImageURl { get; set; }

        [BsonElement("qualifications")]
        public List<Qualification> Qualifications { get; set; }

    }


    public partial class Qualification
    {
        [BsonElement("degree")]
        public string Degree { get; set; }

        [BsonElement("graduated")]
        public long Graduated { get; set; }

        [BsonElement("university")]
        public string University { get; set; }

        [BsonElement("abbr")]
        public string Abbr { get; set; }

        [BsonElement("specilization")]
        public string Specilization { get; set; }

        [BsonElement("specilizationabbr")]
        public string Specilizationabbr { get; set; }

    }
}
