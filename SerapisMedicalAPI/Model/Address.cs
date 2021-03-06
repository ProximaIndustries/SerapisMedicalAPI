using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.DoctorModel.Practice
{
    public class Address
    {

        [BsonElement("addressLineone")]
        public string AddressLineOne { get; set; }


        [BsonElement("addressLinetwo")]
        public string AddressLineTwo { get; set; }


        [BsonElement("citytown")]
        public string CityTown { get; set; }


        [BsonElement("postalcode")]
        public string PostalCode { get; set; }
    }
}
