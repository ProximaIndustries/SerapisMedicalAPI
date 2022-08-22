using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IBilling
    {
        Task<object> CancelSubPlan();

        Task<object> StartSubPlan();
    }
}
