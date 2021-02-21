using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IPayment
    {
        Task<bool> BillClient();

        Task<bool> PauseSubscription();

        Task<bool> CancelSubscription();

        Task<bool> UpdateSubscription();
    }
}
