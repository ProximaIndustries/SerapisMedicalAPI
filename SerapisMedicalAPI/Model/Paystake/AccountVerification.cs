using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.Paystake
{
    public class AccountVerification
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public VerificationData Data { get; set; }
    }

    public class VerificationData
    {
        public string AuthorizationUrl { get; set; }
        public string AccessCode { get; set; }
        public string Reference { get; set; }
    }
}
