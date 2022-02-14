using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.Symptoms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SerapisMedicalAPI.Services.SymptomsChecker
{
    public class SymptomsCheckerService : ISymptomsCheckerService
    {
        private readonly ILogger<SymptomsCheckerService> _logger;
        private Stopwatch timer = new Stopwatch();
        IEnumerable<Symptoms> cachedSymptoms =null;
        public SymptomsCheckerService(ILogger<SymptomsCheckerService> logger)
        {
            _logger = logger;
            cachedSymptoms = GetInstanceAllSymptoms();

        }

        public IEnumerable<Symptoms> GetInstanceAllSymptoms()
        {
            IEnumerable<Symptoms> list = null;
            try
            {
                StringBuilder sb = new StringBuilder();

                //build headers
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add(ConfigConstants.X_RAPIDAPI_HOST_KEY, ConfigConstants.X_RAPIDAPI_HOST_VALUE);
                headers.Add(ConfigConstants.X_RAPIDAPI_KEY_KEY, ConfigConstants.X_RAPIDAPI_KEY_VALUE);
                sb.Append(EndpointConstants.PRIAID_SYMPTOM_CHECKER);
                sb.Append(ConfigConstants.SYMPTOMS_ENDPOINT);
                string url = sb.ToString();

                Debug.WriteLine("URL THAT WILL BE USED " + url.ToString());
                _logger?.LogInformation("URL being Requested: "+ url.ToString());

                timer.Start();
                var responseMessage = APIConector<Symptoms>.GetExternalAPIData(url, headers);
                timer.Stop();
                _logger?.LogInformation("The Response Time of REST call[ "+timer.ElapsedMilliseconds+"ms"+" ] for response code ["+responseMessage.StatusCode+"]");
                timer.Reset();
                
                if (responseMessage.IsSuccessStatusCode)
                {
                    var stringResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine("Final Response message" + stringResponse);
                    
                    list = JsonConvert.DeserializeObject<IEnumerable<Symptoms>>(stringResponse);
                    _logger?.LogInformation("The number of doctors being returned is: {@list} ", list);
                }
                else
                {
                    // do something and fail
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Host terminated unexpectedly");
                throw ex;
                
            }
            return list;
        }

        public IEnumerable<Symptoms> GetAllSymptoms()
        {
            return cachedSymptoms;
        }

        public IEnumerable<DiagnosisResponse> GetProposedDiagnosisBySymptoms(string gender ,string yob,int[] ids)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<DiagnosisResponse> list = null;
            //build headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add(ConfigConstants.X_RAPIDAPI_HOST_KEY, ConfigConstants.X_RAPIDAPI_HOST_VALUE);
            headers.Add(ConfigConstants.X_RAPIDAPI_KEY_KEY, ConfigConstants.X_RAPIDAPI_KEY_VALUE);
            sb.Append(EndpointConstants.PRIAID_SYMPTOM_CHECKER);
            sb.Append(ConfigConstants.DIAGNOSIS_ENDPOINT);
            
            sb.Append("symptoms=");
            var json = JsonConvert.SerializeObject(ids);
            sb.Append(HttpUtility.UrlEncode(json)); // Expecting a string in the format [123,123]//https://priaid-symptom-checker-v1.p.rapidapi.com/diagnosis?language=en-gb&
            sb.Append("&year_of_birth=" + yob);
            sb.Append("&gender=" + gender);
            string url = sb.ToString();
            _logger?.LogInformation("URL being requested:" + url);
            timer.Start();
            var responseMessage = APIConector<Symptoms>.GetExternalAPIData(url, headers);
            timer.Stop();
            _logger?.LogInformation("The Response Time of REST call[ "+timer.ElapsedMilliseconds+"ms"+" ] for response code ["+responseMessage.StatusCode+"]");
            timer.Reset();
            if (responseMessage.IsSuccessStatusCode)
            {
                var stringResponse = responseMessage.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("Final Response message" + stringResponse);
                    
                list = JsonConvert.DeserializeObject<IEnumerable<DiagnosisResponse>>(stringResponse);
                //_logger?.LogInformation("The number of doctors being returned is: {@list} ", list);
                
            }
            else
            {
                // do something and fail
            }
            
            return list;
        }

        public Task GetSepecialistionsBasedOnDiagnosis()
        {
            throw new NotImplementedException();
        }

        public Task GetSymptomsBySublocations()
        {
            throw new NotImplementedException();
        }

    }


}
