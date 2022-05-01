using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Model.Symptoms;
using Serilog;

namespace SerapisMedicalAPI.Services.SymptomsChecker
{
    public class Apimedic :  ISymptomsCheckerService
    {
        private readonly ILogger<Apimedic> _logger;

        public Apimedic()
        {
        }
        public SymptomToken GetToken()
        {
            string uri = "https://sandbox-authservice.priaid.ch/login";
            string api_key = ConfigConstants.SandboxUsername;
            string secret_key = ConfigConstants.SandboxPassword;
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret_key);
            string computedHashString = "";
            using(HMACMD5 hmac = new HMACMD5(secretBytes))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(uri);
                byte[] computedHash = hmac.ComputeHash(dataBytes);
                computedHashString = Convert.ToBase64String(computedHash);
            }
            
            using (WebClient client = new WebClient())
            {
                client.Headers["Authorization"] = string.Concat("Bearer ", api_key, ":", computedHashString);
                try
                {
                    string responseArray = client.UploadString(uri, "POST", "");
                    // Deserialize token string
                    return JsonConvert.DeserializeObject<SymptomToken>(responseArray);
                }
                catch (Exception e)
                {
                    // Exception is in e.Message
                }
            }

            return new SymptomToken();
        }
        public IEnumerable<Symptoms> GetInstanceAllSymptoms()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Symptoms>> GetAllSymptoms()
        {
            IEnumerable<Symptoms> list = null;
            try
            {
                StringBuilder sb = new StringBuilder();

                var tokenResponse = GetToken();
                //build headers
                Dictionary<string, string> headers = new Dictionary<string, string>();
                    //symptoms?token=
                sb.Append(EndpointConstants.APIMEDIC_SANDBOX_BASE_ENDPOINT);
                sb.Append("/symptoms?token=");
                sb.Append(tokenResponse.Token);
                sb.Append("&language=" + ConfigConstants.APILANGUAGE_EN);
                string url = sb.ToString();

                Debug.WriteLine("URL THAT WILL BE USED " + url);
                Log.Information("URL being Requested: "+ url);
                
                var responseMessage = APIConector<Symptoms>.GetExternalAPIData(url, headers);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var stringResponse = await responseMessage.Content.ReadAsStringAsync();
                    Debug.WriteLine("Final Response message" + stringResponse);
                    
                    list = JsonConvert.DeserializeObject<IEnumerable<Symptoms>>(stringResponse);
                    _logger?.LogInformation("The number of doctors being returned is: {@list} ", list);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Host terminated unexpectedly");
                throw ex;
                
            }
            return list;
        }

        public Task GetSymptomsBySublocations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DiagnosisResponse> GetProposedDiagnosisBySymptoms(string gender, string age, int[] ids)
        {
            throw new NotImplementedException();
        }

        public Task GetSepecialistionsBasedOnDiagnosis()
        {
            throw new NotImplementedException();
        }
    }
}