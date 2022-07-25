using SerapisMedicalAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerapisMedicalAPI.Services;

namespace SerapisMedicalAPI.Data
{
    public class BillingRepository : IBilling
    {
        //Sets up a new client for booking flow
        public void OnBoardNewClient()
        {
            //send an email to the client and wait 

            SendGridService.AuthenticateUserEmail();
        }
    }
}
