using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using SerapisMedicalAPI.Services;
using SerapisMedicalAPI.Model;
using System.Reflection;
using System.Diagnostics;

namespace SerapisMedicalAPI.Helpers
{
    //Makes the signiture for payfast authentication.
    public static class SignatureGenerator
    {
        //Data should be a payfast model for making transactions
        public static string GenerateApiSignature()
        {
            string passPhrase = Helpers.ConfigConstants.PayFastTestPassPhrase;

            string result = "";

            BillingDTO dto = new BillingDTO()
            {
                merchant_id = Helpers.ConfigConstants.MerchantIdTest,
                merchant_key= Helpers.ConfigConstants.MerchantKeyTest,
                amount="200",
                item_name="testproduct"
            };

            //list of billing properties
            List<string> data = new List<string>();

            //The list of properties
            PropertyInfo[] properties = typeof(BillingDTO).GetProperties();

            foreach (var prop in properties)
            {
                data.Add(prop.Name + "=" + prop.GetValue(dto).ToString());
            }


            //Check the pass phrase
            if (passPhrase != null)
            {
                //create the payload
                string payload = "";

                //sort the data to alphabetic order
                var sortedData = data.OrderBy(x => x).ToList();

                #region Payfast system variables
                //Add passphrase at the end
                data.Add("passphrase=" + passPhrase);
                #endregion

                //loop through the items and create a new string
                foreach (var key in sortedData)
                {
                    payload += key + "&";
                }

                // Remove the last '&' from the payload
                payload = payload.Substring(0, payload.Length - 1);


                //Make the hash
                using (MD5 md5 = MD5.Create())
                {
                    byte[] input = Encoding.ASCII.GetBytes(payload);

                    byte[] hash = md5.ComputeHash(input);

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        sb.Append(hash[i].ToString("X2"));
                    }

                    result = sb.ToString();

                    return result;
                }
            }
            else
            {
                return "Not generated";
            }
            
        }
    }

    //Connect to the payfast api
}
