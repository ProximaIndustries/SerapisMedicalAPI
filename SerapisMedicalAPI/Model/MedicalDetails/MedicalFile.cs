using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.MedicalDetails
{
    public class MedicalFile
    {
        public string Id { get; set; }
        public string File { get; set; }
        public DateTime DateAndTimeCreated { get; set; }
        public string TypeOfFile { get; set; }
    }
}
