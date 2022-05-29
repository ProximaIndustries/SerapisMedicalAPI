using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IMailing
    {
        void SendConfirmation(string emailAddress);
    }
}
