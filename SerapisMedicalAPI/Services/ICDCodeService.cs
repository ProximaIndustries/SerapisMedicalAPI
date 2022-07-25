using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SerapisMedicalAPI.Helpers;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SerapisMedicalAPI.Services
{
    //Retireves ICD codes from the WHO
    
    public static class ICDCodeService
    {
        private static string tokeEndpoint = "https://icdaccessmanagement.who.int";

        //The credentails must be store in key valut on azure for secuirty, test purposes only
        private static string clientId = ConfigConstants.WHO_CLIENT_ICD_CODE_ID;

        private static string clientSecret = ConfigConstants.WHO_ICD_CODE_API_SECRET;

        private static string scope = "";

        static HttpClient client;


        static HttpRequestMessage request;

        //store them to save and check against the stored token going forward
        private async static Task<string> GetWHOToken()
        {
            client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(tokeEndpoint);

            try
            {
                //check if the disco was successful
                if (disco.IsError)
                {

                    //log the error in the backend rather
                    Debug.WriteLine("Disco error: ", disco.Error);

                    return "error";
                }
                else
                {
                    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                    {
                        Address = disco.TokenEndpoint,
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                        Scope = scope,
                        GrantType = "client_credentials",
                        ClientCredentialStyle = ClientCredentialStyle.AuthorizationHeader
                    });

                    if (tokenResponse.IsError)
                    {
                        //log the error
                        Debug.WriteLine("Token response errro:", tokenResponse.Error);

                        //then return the error
                        return "error";
                    }
                    else
                    {
                        Debug.WriteLine("Token response: ", tokenResponse.Json);

                        return tokenResponse.Json.GetString();
                    }
                }
            }
            catch (Exception)
            {
                return "error";
            }
        }


        #region Code to get the ICD codes
        public async static void GetICDCode(string searchTerm)
        {
            client = new HttpClient();

            //get a token reponse
            var tokenResponse = GetWHOToken().Result;

            //check if the tokenRsponse is valid
            if (tokenResponse != "error")
            {
                client.SetBearerToken(tokenResponse);

                request = new HttpRequestMessage(HttpMethod.Get, $"https://id.who.int/icd/release/11/2021-05/mms/search?q={searchTerm}");

                //Set headers
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                request.Headers.Add("API-Version", "v2");

                //response
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success status code:", response.StatusCode);
                }
                else
                {
                    Debug.WriteLine("****** ICD code and titles from the search *****");

                    var resultJson = response.Content.ReadAsStringAsync().Result;

                    var prettyJson = JValue.Parse(resultJson).ToString(Formatting.Indented);

                    Debug.WriteLine(prettyJson);

                    dynamic searchResult = JsonConvert.DeserializeObject(resultJson);

                    var des = searchResult.destinationEntities;

                    foreach (var de in des)
                    {
                        Debug.WriteLine($"{de.theCode}..........{de.title}");
                    }
                }
            }
        }
        #endregion
    }
}
