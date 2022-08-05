using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.Paystake
{
    public class SubscriptionDTO
    {
        //Email
        public string Customer { get; set; }

        //Definned in the dictionary
        public string Plan { get; set; }

        //From Paystake messagfe object
        public string Authorize { get; set; }
    }
}
