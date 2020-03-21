using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class PracticeContact
    {
        public string TwitterHandle { get; set; }
        public string FacebookHandle { get; set; }
        public string PracticeTelephoneNumber { get; set; }
        public string PracticeEmail { get; set; }
        public string FaxNumber { get; set; }
        public Uri Webiste { get; set; }
    }
}
