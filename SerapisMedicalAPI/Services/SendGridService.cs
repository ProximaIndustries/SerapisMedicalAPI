using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using SerapisMedicalAPI.Helpers;

namespace SerapisMedicalAPI.Services
{
    public static class SendGridService
    {
        private static SendGridClient clientObj = new SendGridClient(ConfigConstants.SendGrid_API_KEY);

        #region Send grid methods
        //sends news letters about Serapis medical
        //Rather a list of emails must be recivied from the api
        public async static Task<object> SendNewsLetter(string emailAddress)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@kbalpha.co.za", "Serapis medical Team"),
                Subject = "Sending with Twilio SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, even with C#",
                HtmlContent = "<strong>and easy to do anywhere, even with C#</strong>",
                
            };
            msg.AddTo(emailAddress, "Testing sendgrid");

            var response = await clientObj.SendEmailAsync(msg).ConfigureAwait(false);

            return response;
        }


        #endregion

        public static string AuthenticateUserEmail()
        {
            return "User email sent";
        }
    }
}
