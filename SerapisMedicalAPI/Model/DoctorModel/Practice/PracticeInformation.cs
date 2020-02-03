using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.DoctorModel.Practice
{
    public class PracticeInformation
    {
        public ObjectId Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string PracticeName { get; set; }

        public Address PracticeAddress { get; set; }

        public double DistanceFromPractice { get; set; }
    }
}
