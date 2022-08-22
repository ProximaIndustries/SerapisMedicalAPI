using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Text;
using Appwrite;

namespace SerapisMedicalAPI.Services
{
    //TODO To be deleted and move all interfaces to the interface folder
    public class APIConector<T>
    {

        private static ILogger<APIConector<T>> _logger;
        public APIConector(ILogger<APIConector<T>> logger)
        {
            _logger = logger;
        }
        //Need to create a mapping for responses
        private static Stopwatch timer = new Stopwatch();
        [Obsolete]
        public static HttpResponseMessage GetExternalAPIData(string endpoint, Dictionary<string, string> headers)
        {
            var query = endpoint;
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
                    client.DefaultRequestHeaders.Add(val.Key, val.Value);
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.BaseAddress = new Uri(query);

                var response = client.GetAsync(query).GetAwaiter().GetResult();

                timer.Stop();

                Log.Information("The Response Time of REST call[ " + timer.ElapsedMilliseconds + "ms" + " ] for response code [" + response.StatusCode + "]");
                timer.Reset();
                return response;
            }
        }
        [Obsolete("This method will be removed soon in favour of the proper async call")]
        public static async Task<HttpResponseMessage> PostExternalAPIData(string endpoint, T content, Dictionary<string, string> headers)
        {
            var postUrl = string.Format(endpoint);
            //var apikey = string.Format("WrYHp6KKSRW0mdq8kVc4rw== ");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                foreach (var val in headers)
                {
                    if (val.Key == "ApiKey")
                    {
                        if (val.Value != "-1")
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(val.Value);
                    }
                    else if (val.Key == "x-rapidapi-key") {
                        if (val.Value != "-1")
                            client.DefaultRequestHeaders.Add(val.Key, val.Value);
                    }
                    else if (val.Key == "Authorization")
                    {
                        if (val.Value != "-1")
                            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", val.Value.ToString());
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
        
    }

    public class ApiConnector : IApiConnector
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private HttpClient http;
        private readonly Dictionary<string, string> headers;
        private readonly Dictionary<string, string> config;
        private string endPoint = "http://178.62.113.15/v1";
        private bool selfSigned = false;
        private HttpClient _httpClient;


        public ApiConnector(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method, Dictionary<string, string> headers = null)
        {
            try
            {
                _httpClient = _httpClientFactory.CreateClient();
                _httpClient.BaseAddress = new Uri(baseAddress);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                if (method == HttpMethod.Post)
                {
                    string data = JsonConvert.SerializeObject(request);
                    HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    return await _httpClient.PostAsync(requestUri, content);
                }
                else if (method == HttpMethod.Get)
                {
                    return await _httpClient.GetAsync(requestUri);
                }
                else if (method == HttpMethod.Put)
                {
                    string data = JsonConvert.SerializeObject(request);
                    HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    return await _httpClient.PutAsync(requestUri, content);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<HttpResponseMessage> AppWriteRequest(string method, string path, Dictionary<string, string> headers, Dictionary<string, object> parameters)
        {
            try
            {
                http = _httpClientFactory.CreateClient();

                bool methodGet = "GET".Equals(method, StringComparison.InvariantCultureIgnoreCase);

                string queryString = methodGet ? "?" + parameters.ToQueryString() : string.Empty;

                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(method), endPoint + path + queryString);

                if ("multipart/form-data".Equals(headers["content-type"], StringComparison.InvariantCultureIgnoreCase))
                {
                    MultipartFormDataContent form = new MultipartFormDataContent();

                    foreach (var parameter in parameters)
                    {
                        if (parameter.Key == "file")
                        {
                            FileInfo fi = parameters["file"] as FileInfo;

                            var file = File.ReadAllBytes(fi.FullName);

                            form.Add(new ByteArrayContent(file, 0, file.Length), "file", fi.Name);
                        }
                        else if (parameter.Value is IEnumerable<object>)
                        {
                            List<object> list = new List<object>((IEnumerable<object>) parameter.Value);
                            for (int index = 0; index < list.Count; index++)
                            {
                                form.Add(new StringContent(list[index].ToString()), $"{parameter.Key}[{index}]");
                            }
                        }
                        else
                        {
                            form.Add(new StringContent(parameter.Value.ToString()), parameter.Key);
                        }
                    }
                    request.Content = form;

                }
                else if (!methodGet)
                {
                    string body = parameters.ToJson();

                    request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                }

                foreach (var header in headers)
                {
                    if (header.Key.Equals("content-type", StringComparison.InvariantCultureIgnoreCase))
                    {
                        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                    }
                    else
                    {
                        if (http.DefaultRequestHeaders.Contains(header.Key)) {
                            http.DefaultRequestHeaders.Remove(header.Key);
                        }
                        http.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                foreach (var header in headers)
                {
                    if (header.Key.Equals("content-type", StringComparison.InvariantCultureIgnoreCase))
                    {
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                    }
                    else
                    {
                        if (request.Headers.Contains(header.Key)) {
                            request.Headers.Remove(header.Key);
                        }
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
                try
                {
                    var httpResponseMessage = await http.SendAsync(request);
                    var code = (int) httpResponseMessage.StatusCode;
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();

                    if (code >= 400) {
                        var message = response.ToString();
                        var isJson = httpResponseMessage.Content.Headers.GetValues("Content-Type").FirstOrDefault().Contains("application/json");

                        if (isJson) {
                            message = (JObject.Parse(message))["message"].ToString();
                        }

                        throw new AppwriteException(message, code, response.ToString());
                    }

                    return httpResponseMessage;
                }
                catch (Exception e)
                {
                    throw new AppwriteException(e.Message, e);
                }
            }
            catch (AppwriteException ex)
            {
                Debug.WriteLine("Appwrite Exception: "+ex);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }


    public interface IApiConnector
    {
        Task<HttpResponseMessage> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method, Dictionary<string, string> headers = null);

        Task<HttpResponseMessage> AppWriteRequest(string method, string path, Dictionary<string, string> headers, Dictionary<string, object> parameters);
    }
    
    
}