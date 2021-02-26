using SerapisMedicalAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Data
{
    public class MailRepository : IMailing
    {
        public void SendConfirmation(string emailAddress)
        {
            MailMessage confirmationEmail = new MailMessage();

            try
            {
                using(MailMessage message=new MailMessage())
                {
                    message.To.Add(emailAddress);
                    message.From = new MailAddress("info@kbalpha.co.za");
                    message.Subject = "Confirm your email";
                    message.Body = "Please click the link to confirm your email address";
                    message.CC.Add("serapismedical@gmail.com");

                    confirmationEmail = message;
                    using (SmtpClient smpt = new SmtpClient("smtpout.secureserver.net"))
                    {
                        smpt.Credentials = new NetworkCredential("info@kbalpha.co.za", "gC72Tr^B!Z9yw");
                        smpt.Send(confirmationEmail);
                    }
                }
            }
            catch (Exception ex)
            {
                //log the problem
                throw ex;
            }
        }
    }
}
