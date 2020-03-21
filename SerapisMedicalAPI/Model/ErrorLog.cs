using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class ErrorLog
    {
        public DateTime LogDateAndTime { get; set; }

        public uint LevelOfSeverity { get; set; }

        public ObjectId UserLoggingId { get; set; }

        public string Tag { get; set; }

        public string Message { get; set; }
    }
}
