using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Utils
{
    public static class EmailSystem
    {
        
        public static async Task<bool> SendSMSbyClickATell(Messaging messageToSend)
        {
            const string url = "https://platform.clickatell.com/v1/message";
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization","CGAmhjXETkq4ZP7DVPkxQQ==");
            var response = await Task.FromResult(APIConector<Messaging>.PostExternalAPIData(url,messageToSend,headers));
            
                return true;
        }
        
        public static async Task<Task<HttpResponseMessage>> SendSMSbyClickATell(ClickATellMessage messageToSend)
        {
            const string url = "https://platform.clickatell.com/v1/message";
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization","CGAmhjXETkq4ZP7DVPkxQQ==");
            var response =
                await Task.FromResult(APIConector<ClickATellMessage>.PostExternalAPIData(url, messageToSend, headers));
            
            return response;
        }
        
        public static string GenerateSession()
        {
            
            //sessionID: IP:randomGeneratedNumber:timestamp
            DateTime t = DateTime.Now;

            return "011" + new Random().Next(100, 999) +
                   t.ToString("yyMMddHHmmss") +
                   t.Ticks.ToString().Substring(t.Ticks.ToString().Length - 12);
        }
        
        public static string Sha256Hash(string data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(data);
                byte[] hashBytes = sha256.ComputeHash(sourceBytes);
                string stringHash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return stringHash;
            }
        }
        
        public static string GenerateRandomCode(int length)
        {
            var sufficientBufferSizeInBytes = (length * 6 + 7) / 8;
            var buffer = new byte[sufficientBufferSizeInBytes];
            RandomNumberGenerator.Create().GetBytes(buffer);
            var result = Convert.ToBase64String(buffer).Substring(0, length);
            return Regex.Replace(result, "[^A-Za-z0-9]", "");
        }
        

    }
}
