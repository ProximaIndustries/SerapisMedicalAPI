using SerapisMedicalAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerapisMedicalAPI.Model.Paystake;

namespace SerapisMedicalAPI.Services
{
    public static class PaystackService 
    {
        // #1
        //create cusotmer
        private async static Task<HttpResponseMessage> CreateCustomer(ClientDTO customerObj)
        {
            try
            {
                //Example for refrence if the properties needed
                //customerObj = "{ \"email\": \"khanyisani.buthelezi03@gmail.com\",\"first_name\": \"Zero\",\"last_name\": \"Sum\",\"phone\": \"+27609887758\"}";

                var dto = JsonConvert.SerializeObject(customerObj);

                using(HttpClient client =new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ConfigConstants.Paystack_Secret_Test}");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(dto);

                    var message = await client.PostAsync($"{ConfigConstants.PayStack_Base_URL}customer", content);

                    return message;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //#2
        //Body must have email and amount of R1 and return a response body witht the authroization code
        private async static Task<HttpResponseMessage> InitalizeTransaction(InitTransaction transactionObj)
        {
            //Get the customer code based on the email first 

            string url = ConfigConstants.PayStack_Base_URL + "transaction/initialize";

            //Construct the http content
            var json = JsonConvert.SerializeObject(transactionObj);

            HttpContent content = new StringContent(json);

            using(HttpClient client =new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                var response = await client.PostAsync(url, content);

                return response;
            }
        }

        //#3
        //pass the body from InitalizeTransaction method
        private async static Task<AccountVerification> VerifyTransaction(string refrence)
        {
            string url = ConfigConstants.PayStack_Base_URL + $"transaction/verify/{refrence}";

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("", "");

                var json = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;

                var response = JsonConvert.DeserializeObject<AccountVerification>(json);

                return response;
            }
        }

        //#4 
        //final
        public async static Task<HttpResponseMessage> CreateSubscriptionPlan(SubscriptionDTO subDto)
        {

            var dto = JsonConvert.SerializeObject(subDto);

            Debug.WriteLine("service type: ", dto);

            #region temp code
            /*//headers
            Dictionary<string, string> httpHeaders = new Dictionary<string, string>() { };

            httpHeaders.Add("Authorization", $"Bearer {ConfigConstants.Paystack_Secret_Test}");
            //Rather use the connector object in production
            //This is temp for testing the flow
            var message = _apiConnector.MakeHttpRequest(
                                                        body, ConfigConstants.PayStack_Base_URL, 
                                                        "subscription", 
                                                        HttpMethod.Post, 
                                                        httpHeaders
                                                        ).Result;*/
            #endregion

            //Check if the customer exiets then Create the customer first
            //Else tell them they subscribed
            //await CreateCustomer(body);

            //if the customer was added then run the next operation

            //Temp code
            try
            {
                using(HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ConfigConstants.Paystack_Secret_Test}");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(dto, Encoding.UTF8, "application/json");

                    var message = await client.PostAsync($"{ConfigConstants.PayStack_Base_URL}subscription", content);

                    return message;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        //#logic to create a new subcriber
        //pass the DTO from the website
        public async static Task<object> AddSubscriber(ClientDTO customerObject)
        {
            //1 check if the client is already subcribed

            //2 If not subcribed create the client
            var clientCreationResponse = CreateCustomer(customerObject);

            //if the creation was successful
            if (clientCreationResponse.Result.IsSuccessStatusCode)
            {
                //Initalize the transaction
                InitTransaction transactionObj = new InitTransaction()
                {
                    Email = customerObject.Email,
                    Amount = ConfigConstants.Paystack_Test_Amount
                };

                var initTransactionResponse = await InitalizeTransaction(transactionObj);

                //check if the init was fine
                if (initTransactionResponse.IsSuccessStatusCode)
                {
                    //set the content of the initalized payment
                    var initJson = initTransactionResponse.Content.ReadAsStringAsync().Result;

                    var initResponse = JsonConvert.DeserializeObject<AccountVerification>(initJson);

                    //Call the verifiy method
                    var subObj = VerifyTransaction(initResponse.Data.Reference);

                    //check if the verification is successfull
                    if (subObj.Result.Status)
                    {
                        SubscriptionDTO dto = new SubscriptionDTO()
                        {
                             Authorize = subObj.Result.Data.AccessCode,
                             Customer = customerObject.Email,
                             Plan = ""
                        };

                        var result = CreateSubscriptionPlan(dto);

                        return result;
                    }
                    else
                    {
                        //error handle
                        return subObj.Result.Message;
                    }
                }
                else
                {
                    //error handle
                    return initTransactionResponse.RequestMessage;
                }
            }
            else
            {
                //error handle
                return clientCreationResponse.Result.RequestMessage;
            }
        }
    }
}
