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
        
        //APIMEDIC API
        public readonly static string SandboxUsername = "serapismedical@gmail.com";
        public readonly static string SandboxPassword = "Px5p4R7GmYg3t2F9X";
        
        public readonly static string LIVEUsername = "Xj8g7_GMAIL_COM_AUT";
        public readonly static string LIVEPassword = "r4S2Hmb5F7Jsp6T8P";
        public readonly static string APILANGUAGE_EN = "en-gb";
        
        //Authorization | token
        //symptoms?token=eyJ0eXAiOiJKV1QiLCJhbGci...&language=en-gb
        
        public readonly static string Firebase_BASE_URL = "https://firebasestorage.googleapis.com";
        public readonly static string Firebase_OAUTH_CREDENTIAL = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key=[API_KEY]";
        
        public readonly static string Cassandra_Diagnosis_By_Symptoms = 
            "CREATE TABLE diagnosis_by_symptoms(diagnosis_id string PRIMARY KEY, diagnosis_description text, diagnosis_count int);";
        //                                      (diagnosis_id string, diagnosis_description text, diagnosis_count int, PRIMARY KEY (diagnosis_id))
        //                                      (userid int, name text, email text, PRIMARY KEY (userid))

        //Payfast passphrase
        public readonly static string PayFastPassPhrase = "Serapismedicalpayments2017";
        public readonly static string MerchantId = "17085895";
        public readonly static string MerchantKey = "etcaix981f9hz";

        //Payfast test credenetials
        public readonly static string PayFastTestPassPhrase = "serapismedicaltest";
        public readonly static string MerchantIdTest = "10026608";
        public readonly static string MerchantKeyTest = "k86ol32slrn4y";

        //Send Grid
        public readonly static string SendGrid_API_KEY = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        public readonly static string Template_ID = "d-33686f907fea4a72934722ee5fd4ac0e";
        public readonly static string Template_Welcom_ID = "5ba3697b-2303-4379-99ee-e9e2a877df75";

        //Paystack
        public readonly static string PayStack_Base_URL = " https://api.paystack.co/";
        public readonly static string Paystack_Secret_Test = "sk_test_136949ba29ab78ae2b9922ed36d6c36009554a3b";
        public readonly static string Paystack_Public_Key = "pk_test_826d84267e550b8967527bf5f794ad24da0f1744";
        public readonly static string Paystack_Test_Amount = "100";
    }
}
