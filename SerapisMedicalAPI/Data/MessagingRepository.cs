using System;
using System.Linq;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Utils;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Data
{
    public class MessagingRepository : IMessagingRepository
    {
        private readonly Context _context = null;
        public MessagingRepository()
        {
            _context = null; // new Context();
            
        }


        public async Task SendSms(Messaging messaging)
        {
            var data = await EmailSystem.SendSMSWithClickATell(messaging);
        }
        
        
        public async Task<BaseResponse<ClickATellMessage>> SendOTPSms(Messaging messaging)
        {
            var message = messaging.messages.FirstOrDefault();
            var otp = OtpHelper.GenerateNewOTP(5);
            messaging.messages.Clear();
            
            message.content = $"Serapis Medical: Your OTP is {otp}";
            
            messaging.messages.Add(message);
            var data = await EmailSystem.SendSMSWithClickATell(messaging);

            return new BaseResponse<ClickATellMessage> {StatusCode = "200"};

        }
        public Task SendSingleSms(ClickATellMessage messaging)
        {
            //EmailSystem email = new EmailSystem();
            var x = EmailSystem.SendSMSbyClickATell(messaging);

            return x;
        }

        public Task SendEmail()
        {
            return null;
        }
    }

    public static class OtpHelper
    {

        public static string GenerateNewOTP(int iOTPLength)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };  
            string sOTP = String.Empty;  
  
            string sTempChars = String.Empty;  
  
            Random rand = new Random();  
  
            for (int i = 0; i < iOTPLength; i++)  
  
            {  
  
                int p = rand.Next(0, saAllowedCharacters.Length);  
  
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];  
  
                sOTP += sTempChars;  
  
            }  
  
            return sOTP;
        }
    }
}