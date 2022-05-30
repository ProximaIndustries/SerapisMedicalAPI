using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        
        public byte[] ObjectToByteArray<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                var objectToByteArray = ms.ToArray();
                return objectToByteArray;
            }
        }
        /// <summary>
        /// Convert a byte array to an object of the Type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        public T ByteArrayToObject<T>(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                T obj = (T)new BinaryFormatter().Deserialize(memStream);
                return obj;
            }
        }
    }
}