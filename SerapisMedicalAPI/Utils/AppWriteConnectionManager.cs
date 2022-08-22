using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Appwrite;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SerapisMedicalAPI.Utils
{
    public class AppWriteConnectionManager
    {
        private ILogger<AppWriteConnectionManager> _logger;

        
        public AppWriteConnectionManager(ILogger<AppWriteConnectionManager> logger)
        {
            _logger = logger;
            _logger?.LogInformation("Attempting to connect to the Cassandra Cluster");
            
            
            
            
                
            GetSession = Invoke();
            
        }
        
        public Client Invoke()
        {
            try
            {
                var client = new Client();
                client
                    .SetEndPoint("http://178.62.113.15/v1") // Make sure your endpoint is accessible
                    .SetProject("62ed1c6510430beb464d") // Your project ID
                    .SetKey("a92f99e14cbcc5dd1792ce80ad2a665df70af41491d59d382f04311572341bf3c7e2b4d9a605ab70a9a231069af95093e946b87a49662fcab380365e490d6b7064b7732dfb6c76c95f03e7fb680f8b5d6318884e8186af9c7c23d35634dfac7e381bc51ce049b909f7a7dadc118cda087afb2b8df215c77af9e8ee78bf3dac63")
                    .SetSelfSigned(true); // Use only on dev mode with a self-signed SSL cert
            
                _logger?.LogWarning("Connect to Appwrite!:  ");
                return client;
            }
            catch (Exception e)
            {
                _logger?.LogError("Could not Connect to Appwrite: "+JsonConvert.SerializeObject(e));
                throw;
            }
        }
        
        
        public Client GetSession { get; }
    }
}