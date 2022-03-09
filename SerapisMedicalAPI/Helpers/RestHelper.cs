using System;
using System.Collections.Generic;

namespace SerapisMedicalAPI.Helpers
{
    public class RestHelper
    {
        
        public Dictionary<string, string> GetHeaders(string nonce)
        {
            return GetHeaders(nonce, DateTime.UtcNow, false);
        }

        private Dictionary<string, string> GetHeaders(string nonce, DateTime dateTime, bool asRetry)
        {
            //var signature = GenerateSignature(nonce, dateTime);

            var headers = new Dictionary<string, string>
            {
                { "Authorization", "" },
                { "Date", dateTime.ToString("R") },
                { "x-mod-nonce", nonce },
                { "x-mod-retry", asRetry.ToString().ToLower() }
            };

            return headers;
        }
    }
}