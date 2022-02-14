using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Helpers
{
    public class ConfigConstants
    {
        //https://apimedic.com/#product
        public readonly static string SYMPTOMS_ENDPOINT = "/symptoms?language=en-gb&format=json";
        public readonly static string DIAGNOSIS_ENDPOINT = "/diagnosis?language=en-gb&";
        public readonly static string X_RAPIDAPI_HOST_VALUE = "priaid-symptom-checker-v1.p.rapidapi.com";
        public readonly static string X_RAPIDAPI_KEY_VALUE = "0277c06f30msh281385d270800e2p19c20ejsn00e0275e805a";
        public readonly static string X_RAPIDAPI_HOST_KEY = "X-RapidAPI-Host";
        public readonly static string X_RAPIDAPI_KEY_KEY = "X-RapidAPI-Key";
    }
}
