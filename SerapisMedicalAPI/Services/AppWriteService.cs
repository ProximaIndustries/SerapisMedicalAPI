using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerapisMedicalAPI.Model.AppWrite;
using SerapisMedicalAPI.Services.SymptomsChecker;
using SerapisMedicalAPI.Utils;
using Serilog;

namespace SerapisMedicalAPI.Services
{
    public class AppWriteService : IAppWriteService
    {
        private readonly IApiConnector _connector;
        private Dictionary<string, string> _headers;
        public AppWriteService(IApiConnector connector)
        {
            _connector = connector;
            _headers = new Dictionary<string, string>()
            {
                {"content-type", "application/json"},
                {"x-sdk-version", "appwrite:dotnet:0.3.0"},
                {"X-Appwrite-Response-Format", "0.9.0"},
                //{"X-Appwrite-Key", "a92f99e14cbcc5dd1792ce80ad2a665df70af41491d59d382f04311572341bf3c7e2b4d9a605ab70a9a231069af95093e946b87a49662fcab380365e490d6b7064b7732dfb6c76c95f03e7fb680f8b5d6318884e8186af9c7c23d35634dfac7e381bc51ce049b909f7a7dadc118cda087afb2b8df215c77af9e8ee78bf3dac63"},
                {"X-Appwrite-Key", "1191c8de67010e3446ef8f45af8259786bf9a114785c571cdeebc43512f7ee45a534e0bb092b30f87d40fb203a2790849ea9e0250bc70d147b7a898688d22ccd7f49079d1c67b38648d685403b792771a4d8ebe6a24836f880ab1f9faf3ff1322a703f92ad3983c44629293ee702cee686e3fb4a10ff7bcd1b3fcbf38d70a03a"},
                {"X-Appwrite-Project", "62ed1c6510430beb464d"},
                //{"jWT", "value"}
            };
        }


        /// <summary>
        /// Account Creation
        /// </summary>
        public async Task<HttpResponseMessage> CreateAccount(AppWriteAuthRequest request) 
        {
            string path = "/account";
            
            
            
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "userId", "unique()" },
                { "email", request.Email },
                { "password", request.Password },
                { "name", request.Name }
            };
            
            return await _connector.AppWriteRequest("POST", path, _headers, parameters);
            //return await _connector.MakeHttpRequest(request,"http://178.62.113.15/v1",path,HttpMethod.Post);

        } 
        public async Task<HttpResponseMessage> CreateJwt() 
        {
            string path = "/account/jwt";
            
            
            
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
            };
            
            return await _connector.AppWriteRequest("POST", path, _headers, parameters);
            //return await _connector.MakeHttpRequest(request,"http://178.62.113.15/v1",path,HttpMethod.Post);

        }
        
        public async Task<AppWriteSession> CreateAccountSessionByEmail(string jwt,AppWriteAuthRequest request) 
        {
            //Login basically
            string path = "/v1/account/sessions/email";

            if (!string.IsNullOrEmpty( jwt ))
            {
                _headers.Add("jWT",jwt);
            }
            
            
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"email",request.Email},
                {"password",request.Password}
            };
            
            var httpResponse =  await _connector.AppWriteRequest("POST", path, _headers, parameters);
            if (httpResponse is null || !httpResponse.IsSuccessStatusCode)
            {
                Log.Warning($"Unsuccessful Calling ");
            }
            string stringResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AppWriteSession>(stringResponse);
            //return await _connector.MakeHttpRequest(request,"http://178.62.113.15/v1",path,HttpMethod.Post);

        }
        public async Task<HttpResponseMessage> CreateAccountSessionByPhone(AppWriteAuthRequest request) 
        {
            //Login basically
            string path = "/v1/account/sessions/phone";
            
            
            
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
            };
            
            return await _connector.AppWriteRequest("POST", path, _headers, parameters);
            //return await _connector.MakeHttpRequest(request,"http://178.62.113.15/v1",path,HttpMethod.Post);

        }
    }
}