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
            //EmailSystem email = new EmailSystem();
            var data = await EmailSystem.SendSMSbyClickATell(messaging);
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
}