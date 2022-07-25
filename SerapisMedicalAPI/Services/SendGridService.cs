using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Model;


namespace SerapisMedicalAPI.Services
{
    public static class SendGridService
    {
        private static SendGridClient clientObj = new SendGridClient(ConfigConstants.SendGrid_API_KEY);

        #region Send grid methods
        public static async Task<object> AddContactEnquiry(ContactDTO dto)
        {
            var _dto = JsonConvert.SerializeObject(dto);

            try
            {
                var response = await clientObj.RequestAsync(
                                                            method: SendGridClient.Method.PUT,
                                                            requestBody: _dto,
                                                            urlPath: "marketing/contacts"
                                                            );



                return "Status code:" + response.StatusCode;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task SendEmailAsync(string emailAddress)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("serapismedical@gmail.com", "Serapis medical Team"),
                Subject = "Sending with Twilio SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, even with C#",
                HtmlContent = "<strong>and easy to do anywhere, even with C#</strong>"
            };
            msg.AddTo(emailAddress, "Testing sendgrid");

            var response = await clientObj.SendEmailAsync(msg).ConfigureAwait(false);
        }

        //sends news letters about Serapis medical
        //Rather a list of emails must be recivied from the api
        public async static Task<object> SendNewsLetter(List<string> emailAddresses)
        {
            //loop through the list and send the newletter
            return null;
        }


        //flow to authenticate the user for subscribing to a plan.
        public async static void NewSubcriberEmailAuthentication(string _email, object _clientData)
        {
            //convert the data to a string using the json serilizaiton method
            var data = JsonConvert.SerializeObject(_clientData);

            //The template for the new client flow
             const string templateId = "d-33686f907fea4a72934722ee5fd4ac0e";

            //call the local private method to update a new template
            var response = CreateEmailTemplate(templateId, data, $"templates/{templateId}/ versions");

            //Temp code
            if (!response.Result.IsSuccessStatusCode)
            {
                //call the send email method
                await SendEmailAsync(_email);

                //do some logging
            }
            else
            {
                //log and alert developer
            }
        }

        #endregion

        //return a template
        private async static Task<Response> CreateEmailTemplate(string _templateId, string _data, string _urlPath)
        {
            var response = await clientObj.RequestAsync(method: BaseClient.Method.PATCH, 
                                                   requestBody: _data, 
                                                   urlPath: _urlPath
                                                   );

            return response;
        }

        public static string AuthenticateUserEmail()
        {
            return "User email sent";
        }
    }
}
