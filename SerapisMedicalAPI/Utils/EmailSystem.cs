using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Utils
{
    public class EmailSystem
    {
        
        public async Task<bool> SendSMSbyClickATell(Messaging messageToSend)
        {
            const string url = "https://platform.clickatell.com/v1/message";

            var response = await Task.FromResult(APIConector<Messaging>.PostExternalAPIData(url,messageToSend, "AfMDkjnITRaKOQKiV6mN_g=="));
            
            return true;
        }
    }
}
