using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IMessagingRepository
    {
        Task SendEmail();
        Task SendSms(Messaging messaging);
        Task<BaseResponse<ClickATellMessage>> SendOTPSms(Messaging messaging);
        Task SendSingleSms(ClickATellMessage messaging);

    }
}
