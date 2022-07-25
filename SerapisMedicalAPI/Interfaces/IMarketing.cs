using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IMarketing
    {
        Task<object> AddContactToSendGridDatabase(Contact dto);

        Task SendNewLetterToClients(IList<string> emailAddresses);
    }
}
