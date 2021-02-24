using System.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace SerapisMedicalAPI.Services
{
    public class APIConector
    {
        //Need to create a mapping for responses
        public bool Connector(ref object _object,string _url, string _data )
        {
            using( HttpClient _httpClient = new HttpClient() )
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(_object);

                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                
                var response = client.PostAsync(_url, content); //add your requesturi as a string
                _httpClient.Dispose();
                
                client.Dispose();
            }


            return false;
        }
    }
}