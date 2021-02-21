using SerapisMedicalAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SerapisMedicalAPI.Utils;

namespace SerapisMedicalAPI.Services
{
    public class PaymentManager : IPayment
    {
        //sandbox
        const string sandbox = "https://api.payfast.co.za/subscriptions/​[token]/[action]?testing=true";

        const string baseUrl = "https://api.payfast.co.za/ping?testing=true";

        //Should start the process
        public Task<bool> BillClient()
        {
            
            try
            {
                using(HttpClient client=new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.BaseAddress = new Uri(baseUrl);

                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(, "");

                    client.DefaultRequestHeaders.TryAddWithoutValidation("merchant-id", Constants.PayfastMerchantID);

                    client.DefaultRequestHeaders.TryAddWithoutValidation("version", "v1");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("timestamp", "");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("signature", "");

                    HttpResponseMessage response = client.GetAsync(baseUrl).GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        return Task.FromResult(true);
                    }
                    else
                    {
                        return Task.FromResult(false);
                    }
                }
            }
            catch (Exception)
            {
                //log error
                throw;
            }
        }


        //Cancels the clients subscription
        public Task<bool> CancelSubscription()
        {
            throw new NotImplementedException();
        }

        //Pauses the subscription in case of disputs
        public Task<bool> PauseSubscription()
        {
            throw new NotImplementedException();
        }

        //Upgrading or downgrading the package
        public Task<bool> UpdateSubscription()
        {
            throw new NotImplementedException();
        }
    }
}
