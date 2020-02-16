using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.DoctorModel.Doctor
{
    public class DoctorDto
    {
        public ObjectId DoctorId { get; set; }

        public string ProfilePicture { get; set; }

        public string FullName { get; set; }

        public string University { get; set; }

        public string TimeAvailable { get; set; }
    }
}
