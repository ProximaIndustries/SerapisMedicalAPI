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
        public Task<object> CancelSubPlan()
        {
            throw new NotImplementedException();
        }

        public async Task<object> StartSubPlan()
        {
            return "Start the sub plan";
        }
    }
}
