using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SerapisMedicalAPI.Services
{
    public class FirebaseService
    {
        private readonly ILogger<FirebaseService> _logger;
        
        public FirebaseService(ILogger<FirebaseService> logger)
        {
            _logger = logger;

        }


        public void AuthenticateUser()
        {
            StringBuilder sb = new StringBuilder();
            //build headers
            Dictionary<string, string> headers = new Dictionary<string, string>();

            string url = sb.ToString();
            var responseMessage = APIConector<object>.PostExternalAPIData(url,null, headers);

        }
        
    }
}