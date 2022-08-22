using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Appwrite;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model.AppWrite;
using SerapisMedicalAPI.Services.SymptomsChecker;
using SerapisMedicalAPI.Utils;

namespace SerapisMedicalAPI.Data.Appwrite
{
    public class AccountAppwriteRepository : IAccountAppwriteRepository
    {
        private readonly AppWriteConnectionManager _appWriteConnection;
        private readonly IAppWriteService _appWriteService;
        private readonly ILogger<AccountAppwriteRepository> _logger;
        public AccountAppwriteRepository(AppWriteConnectionManager appWriteConnection, IAppWriteService appWriteService,
            ILogger<AccountAppwriteRepository> logger)
        {
            _appWriteConnection = appWriteConnection;
            _appWriteService = appWriteService;
            _logger = logger;
        }


        public void TestConnection()
        {
            var client = _appWriteConnection.GetSession;

            var u = new Users(client);
        }

        public async Task RegisterUser(AppWriteAuthRequest request)
        {
            var client = _appWriteConnection.GetSession;

            var accountService = new Users(client);
            var x  = await accountService.Create(request.Email, request.Password, request.Name);

            var response =  await _appWriteService.CreateAccount(request);
            
            _logger.LogInformation($"Response code: {response.StatusCode}");
            if (response.StatusCode == HttpStatusCode.Created)
            {
                string resp = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<AppWriteAuthResponse>(resp);
                _logger.LogInformation($"Response code: {response.StatusCode}");
            }
            
        }

        public async Task<AppWriteSession> LoginUser(AppWriteAuthRequest request)
        {
            //var jwtResponse = await _appWriteService.CreateJwt();
            //var jwt = JsonConvert.DeserializeObject<AppWriteJWT>( (await jwtResponse.Content.ReadAsStringAsync()));
            
            //var client = _appWriteConnection.GetSession;
            return await _appWriteService.CreateAccountSessionByEmail(null, request);
            
        }
    }
}