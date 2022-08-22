using System.Net.Http;
using System.Threading.Tasks;
using SerapisMedicalAPI.Model.AppWrite;

namespace SerapisMedicalAPI.Services.SymptomsChecker
{
    public interface IAppWriteService
    {
        Task<HttpResponseMessage> CreateAccount(AppWriteAuthRequest request);
        Task<HttpResponseMessage> CreateJwt();
        Task<AppWriteSession> CreateAccountSessionByEmail(string jwt, AppWriteAuthRequest request);
        Task<HttpResponseMessage> CreateAccountSessionByPhone(AppWriteAuthRequest request);
    }
}