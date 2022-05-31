using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.MedicalDetails
{
    public class Medication
    {
        [BsonElement("drugcode")]
        public string DrugCode { get; set; }
        
        [BsonElement("dosage")]
        public string Dosage { get; set; }

        [BsonElement("medicationname")]
        public string MedicationName { get; set; }

        [BsonElement("brandname")]
        public string BrandName { get; set; }

        [BsonElement("medicationtypevalue")]
        public MedicationType MedicationTypeValue { get; set; }
        
    }
}
