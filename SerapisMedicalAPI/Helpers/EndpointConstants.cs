using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Helpers
{
    public class EndpointConstants
    {
        public readonly static string PRIAID_SYMPTOM_CHECKER = "https://priaid-symptom-checker-v1.p.rapidapi.com";
        
        //APIMEDIC
        public readonly static string APIMEDIC_AUTHBASE_ENDPOINT = "https://sandbox-authservice.priaid.ch/login";
        //public readonly static string APIMEDIC_AUTHBASE_ENDPOINT = "https://sandbox-authservice.priaid.ch/login";
        public readonly static string APIMEDIC_BASE_ENDPOINT = "https://healthservice.priaid.ch";
        public readonly static string APIMEDIC_SANDBOX_BASE_ENDPOINT = "https://sandbox-healthservice.priaid.ch";


        //Payfast
        public readonly static string PAYFAST_BASE_ENDPOINT = "https://api.payfast.co.za";

        //Supabase
        public readonly static string SUPABASE_BASE_URL = "https://cgwlfswslvrkhvrrxckz.supabase.co";
        public readonly static string SUPABASE_SIGN_UP_WITH_CELL = "/auth/v1/signup";
        public readonly static string SUPABASE_LOGIN_IN_WITH_CELL = "/auth/v1/token?grant_type=password";
    }
}
