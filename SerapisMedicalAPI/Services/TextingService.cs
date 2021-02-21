using Newtonsoft.Json;
using SerapisMedicalAPI.Enums;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Services
{
    public static class TextingService
    {

        static string APIBasicPath = "https://platform.clickatell.com/v1/message/{0}";

        public async static Task<bool> SendMessageAsync(string pvtNumber, string pvtchannel, string pvtcontent)
        {
            pvtchannel = MessageType.sms.ToString();

            try
            {

                List<Message> pvtmessages = new List<Message>()
                {
                     new Message
                     {
                        channel = pvtchannel,
                        content = pvtcontent,
                        to = pvtNumber
                     }
                };

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.BaseAddress = new Uri(APIBasicPath);

                    var model = new Clickatell()
                    {
                        messages= pvtmessages
                    };

                    var json = JsonConvert.SerializeObject(model);

                    APIBasicPath = string.Format(APIBasicPath, json);

                    HttpContent content = new StringContent(json);

                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync(APIBasicPath, content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                //Log error
                throw ex;
            }
        }
    }
}
