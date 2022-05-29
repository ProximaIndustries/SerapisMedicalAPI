using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.PracticeModel
{
    public class PracticeDto
    {
        public string PracticeName { get; set; }

        public string PracticePicture { get; set; }

        public ObjectId PracticeID { get; set; }

        public long Distance { get; set; }

        public int NumberOfPatientsAtPractice { get; set; }

        public string OperatingTimes { get; set; }

        public string ContactNumber { get; set; }
    }
}
