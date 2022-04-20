﻿using System;
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
        public readonly static string Firebase_BASE_URL = "https://firebasestorage.googleapis.com";
        public readonly static string Firebase_OAUTH_CREDENTIAL = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key=[API_KEY]";
        
        public readonly static string Cassandra_Diagnosis_By_Symptoms = "CREATE TABLE diagnosis_by_symptoms(diagnosis_id string PRIMARY KEY, diagnosis_description text, diagnosis_count int);";
    }
}