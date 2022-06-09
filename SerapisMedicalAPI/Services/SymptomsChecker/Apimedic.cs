using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
                    _logger.LogError(e, "Host terminated unexpectedly");
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

        /// <summary>
        /// /diagnosis?token=eyJ0eXAiOiJKV1QiLCJhbGci...&language=de-ch&symptoms=[233]&gender=male&year_of_birth=1988
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="age"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<DiagnosisResponse>> GetProposedDiagnosisBySymptoms(string gender, string age, int[] ids)
        {
            IEnumerable<DiagnosisResponse> list = null;
            try
            {
                StringBuilder sb = new StringBuilder();

                var tokenResponse = GetToken();
                //build headers
                Dictionary<string, string> headers = new Dictionary<string, string>();
                //symptoms?token=
                
                sb.Append(EndpointConstants.APIMEDIC_SANDBOX_BASE_ENDPOINT);
                sb.Append("/diagnosis?token=");
                sb.Append(tokenResponse.Token);
                sb.Append("&language=" + ConfigConstants.APILANGUAGE_EN);
                
                var jsonofIds = JsonConvert.SerializeObject(ids);
                sb.Append("&symptoms=" +jsonofIds);
                
                sb.Append("&gender=" +gender);
                sb.Append("&year_of_birth=" +age);
                
                string url = sb.ToString();

                Debug.WriteLine("URL THAT WILL BE USED " + url);
                Log.Information("URL being Requested: "+ url);
                
                var responseMessage = APIConector<DiagnosisResponse>.GetExternalAPIData(url, headers);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var stringResponse = await responseMessage.Content.ReadAsStringAsync();
                    Debug.WriteLine("Final Response message" + stringResponse);
                    
                    list = JsonConvert.DeserializeObject<IEnumerable<DiagnosisResponse>>(stringResponse);
                    _logger?.LogInformation("The number of doctors being returned is: {@list} ", list);
                    
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Host terminated unexpectedly");
                return await Task.FromResult(list);
                //throw ex;

            }
            return list;
        }

        public Task GetSepecialistionsBasedOnDiagnosis()
        {
            throw new NotImplementedException();
        }

        


        /// <summary>
        /// /symptoms/proposed
        /// </summary>
        /// <param name="ids"></param>
        /// <returns><list type="Symptoms"></list>/returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Symptoms>> GetProposedSymptoms(string gender, string age,string id)
        {
            IEnumerable<Symptoms> list = null;
            try
            {
                //5-2-1
                var strings = id.Split("-").ToArray();
                //var strings = arrStrings[0].Split(",").ToArray();
                
                var arr = Array.ConvertAll(strings, int.Parse);
                
                StringBuilder sb = new StringBuilder();

                var tokenResponse = GetToken();
                //build headers
                Dictionary<string, string> headers = new Dictionary<string, string>();
                //symptoms?token=
                sb.Append(EndpointConstants.APIMEDIC_SANDBOX_BASE_ENDPOINT);
                sb.Append("/symptoms/proposed?token=");
                sb.Append(tokenResponse.Token);
                sb.Append("&language=" + ConfigConstants.APILANGUAGE_EN);
                
                var jsonofIds = JsonConvert.SerializeObject(arr);
                sb.Append("&symptoms=" +jsonofIds);
                
                sb.Append("&gender=" +gender);
                sb.Append("&year_of_birth=" +age);
                string url = sb.ToString();

                Debug.WriteLine("URL THAT WILL BE USED " + url);
                Log.Information("URL being Requested: "+ url);
                
                var responseMessage = APIConector<List<Symptoms>>.GetExternalAPIData(url, headers);

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
    }
}