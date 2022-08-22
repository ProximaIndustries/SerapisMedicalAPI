using System.Threading.Tasks;
using SerapisMedicalAPI.Model.AppWrite;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IAccountAppwriteRepository
    {
        void TestConnection();
        Task RegisterUser(AppWriteAuthRequest request);
        Task<AppWriteSession> LoginUser(AppWriteAuthRequest request);
    }
}