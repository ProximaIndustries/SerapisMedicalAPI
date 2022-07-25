using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Data
{
    public class MarketingRepository : IMarketing
    {
        //Adds a contact which filled the enquiry form on the website
        public async Task<object> AddContactToSendGridDatabase(Contact dto)
        {
            //Call the sendgrid serivces to add the contact to the database
            ContactDTO _dto = new ContactDTO();


            //Initalize the list
            _dto.Contacts = new List<Contact>();

            _dto.Contacts.Add(dto);

            return await Services.SendGridService.AddContactEnquiry(_dto);
        }


        //Sends a newsletter to the contacts that are subscribed to get news letters
        public Task SendNewLetterToClients(IList<string> emailAddresses)
        {
            throw new NotImplementedException();
        }
    }
}
