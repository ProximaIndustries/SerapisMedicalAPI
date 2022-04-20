using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Helpers
{
    public class EndpointConstants
    {
        public readonly static string PRIAID_SYMPTOM_CHECKER = "https://priaid-symptom-checker-v1.p.rapidapi.com";
        
        
        //Supabase
        public readonly static string SUPABASE_BASE_URL = "https://cgwlfswslvrkhvrrxckz.supabase.co";
        public readonly static string SUPABASE_SIGN_UP_WITH_CELL = "/auth/v1/signup";
        public readonly static string SUPABASE_LOGIN_IN_WITH_CELL = "/auth/v1/token?grant_type=password";
    }
}
