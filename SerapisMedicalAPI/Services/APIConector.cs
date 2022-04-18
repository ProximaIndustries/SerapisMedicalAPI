using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SerapisMedicalAPI.Services
{
    public class APIConector<T>
    {
        private static ILogger<APIConector<T>> _logger;
        public APIConector(ILogger<APIConector<T>> logger)
        {
            _logger = logger;

        }
        //Need to create a mapping for responses
        private static Stopwatch timer = new Stopwatch();


        public bool Connector(ref object _object,string _url, string _data )
        {
            using( HttpClient _httpClient = new HttpClient() )
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(_object);

                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                
                var response = client.PostAsync(_url, content); //add your requesturi as a string
                _httpClient.Dispose();
                
                client.Dispose();
            }


            return false;
        }

        public static HttpResponseMessage GetExternalAPIData(string endpoint, Dictionary<string, string> headers)
        {
            var query =endpoint;
            timer.Start();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                foreach (var val in headers)
                {
                    if (val.Key == "ApiKey")
                    {
                        if (val.Value != "-1")
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(val.Value);
                    }
                    else if (val.Key == "X-RapidAPI-Key")
                    {
                        if (val.Value != "-1")
                            client.DefaultRequestHeaders.Add(val.Key, val.Value);
                    }
                    else if (val.Key == "X-RapidAPI-Host")
                    {
                        if (val.Value != "-1")
                            client.DefaultRequestHeaders.Add(val.Key, val.Value);
                    }
                    //add anyways
                    client.DefaultRequestHeaders.Add(val.Key,val.Value);
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                client.BaseAddress = new Uri(query);

                HttpResponseMessage response = client.GetAsync(query).GetAwaiter().GetResult();
                
                timer.Stop();
                
                Log.Information("The Response Time of REST call[ "+timer.ElapsedMilliseconds+"ms"+" ] for response code ["+response.StatusCode+"]");
                timer.Reset();
                return response;
            }
        }
        [Obsolete("This method will be removed soon in favour of the proper async call")]
        public async static Task<HttpResponseMessage> PostExternalAPIData(string endpoint, T content, Dictionary<string,string> headers)
        {
            var postUrl = string.Format(endpoint);
            //var apikey = string.Format("WrYHp6KKSRW0mdq8kVc4rw== ");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                foreach(var val in headers)
                {
                    if(val.Key == "ApiKey")
                    {
                        if (val.Value != "-1")
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(val.Value);
                    }
                    else if(val.Key == "x-rapidapi-key"){
                        if (val.Value != "-1")
                            client.DefaultRequestHeaders.Add(val.Key, val.Value);
                    }
                    else if (val.Key == "Authorization")
                    {
                        if (val.Value != "-1")
                            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",val.Value.ToString());
                    }
                    //client .DefaultRequestHeaders.Add(val.Key,val.Value);
                }

                client.BaseAddress = new Uri(postUrl);

                var json = JsonConvert.SerializeObject(content);

                HttpContent objectConent = new StringContent(json);
                objectConent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var message = await client.PostAsync(postUrl, objectConent);

                return message;
            }
        }
        
        /*public async Task<IRestResponse>MakeHttpRequestWithRestSharp(object requestModel, string baseAddress, string requestUri, RestSharp.Method method, Dictionary<string, string> headers = null)
        {
            try
            {
                var client = new RestClient(baseAddress);
                var request = new RestRequest(requestUri, method);
                var requestBody = JsonConvert.SerializeObject(requestModel);
                request.AddParameter("application/json", requestBody, ParameterType.RequestBody);

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }

                return await client.ExecuteAsync(request);
            }
            catch (Exception ex)
            {

                Log.Error("MakeHttpRequestWithRestSharp", ex.ToString());
                return null;
            }
        }*/
    }
}