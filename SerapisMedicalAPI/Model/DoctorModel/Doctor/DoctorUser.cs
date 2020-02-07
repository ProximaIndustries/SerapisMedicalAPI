using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.DoctorModel.Doctor
{
    public class DoctorUser
    {
        public ObjectId UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string GivenCode { get; set; }
        public string LastLogin { get; set; }
        public bool HasbeenValidated { get; set; }
    }
}
