using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Newtonsoft.Json;
using SerapisMedicalAPI.Data.Base;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Helpers.Config;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.PatientModel;
using SerapisMedicalAPI.Model.Supabase;
using SerapisMedicalAPI.Services;
using SerapisMedicalAPI.Services.SymptomsChecker;
using Serilog;
using Supabase.Gotrue.Responses;

namespace SerapisMedicalAPI.Data.Supabase
{
    public class AccountSupabaseRepository : IAccountSupabaseRepository
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOptions<SupabaseConfig> _options;
        private readonly IMessagingRepository _messagingRepository;
        
        public AccountSupabaseRepository(IAccountRepository accountRepository, IOptions<SupabaseConfig> options, IMessagingRepository messagingRepository)
        {
            _accountRepository = accountRepository;
            _options = options;
            _messagingRepository = messagingRepository;
        }
        public async Task<BaseResponse<Patient>> RegisterUser(Patient patient)
        {
            try
            {
                var service = new SupabaseService();
                var response = await service.RegisterUser(patient, _options.Value.Url,_options.Value.Key);
                if (!response.status)
                {
                    return  new BaseResponse<Patient>() { status = false, message = $"failed to register on supabase: {response.message}",StatusCode = "1"};
                }
                //Update MongoDB
                var userId = response.data.User.Id;
                patient.SocialID = userId;
                patient.Token = string.Empty;
                var mongodbResponse =  _accountRepository.AddPatientAccount(patient);

                return !mongodbResponse.status ?
                    new BaseResponse<Patient>() { status = false, message = "mongodb failed"} : new BaseResponse<Patient>() { data = mongodbResponse.data, status = true, message = "mongo success"};
            }
            catch (Exception e)
            {
                Log.Information(e.ToJson());
                return  new BaseResponse<Patient>() { status = false, message = $"something went wrong {e.ToString()}"};
            }
            
            
        }
        
        public async Task<BaseResponse<PatientAuthResponse<Patient>>> LoginUser([FromBody] SupabaseAuth user)
        {
            try
            {
                SupabaseService service = new SupabaseService();
                var response = await service.LoginUser(user, _options.Value.Url,_options.Value.Key);
                if (!response.status)
                {
                    return  new BaseResponse<PatientAuthResponse<Patient>>{ status = response.status, message = $"supabase {response.message}", StatusCode = StatusCodes.DatabaseError};
                }
                //Update MongoDB
                var userId = response.data.User.Id;
                
                Log.Information("Supabase User "+response.ToJson());
                user.password = string.Empty;
                var mongodbResponse = await  _accountRepository.GetUserById(userId);
                
                var otp = OtpHelper.GenerateNewOTP(5);
                
                var message = new ClickATellMessage
                {
                    to = mongodbResponse.data.PatientContactDetails.CellphoneNumber,
                    channel = "sms",
                    content = $"Serapis Medical: \n Your OTP is {otp}"
                };

                var  messaging = new Messaging()
                {
                    messages = new List<ClickATellMessage>()
                };
                messaging.messages.Add(message);
                bool isMessageSuccessful = await _messagingRepository.SendSms(messaging);
                Log.Information($"Was OTP: {otp} sent out to {message.to} ? : {isMessageSuccessful} ");
                return new BaseResponse<PatientAuthResponse<Patient>>()
                {
                    status = true,
                    StatusCode = StatusCodes.Successful,
                    message = "Success on Supabase and Mongodb ",
                    data = new PatientAuthResponse<Patient>
                    {
                        Data = mongodbResponse.data,
                        SupabaseData = response.data,
                        Otp = otp
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse<PatientAuthResponse<Patient>>()
                    {message = e.ToString(), status = false, data = null, StatusCode = StatusCodes.FatalError};
            }
        }

        public async Task<BaseResponse<PatientAuthResponse<Doctor>>> LoginSSOUser(SupabaseAuth user)
        {
            try
            {
                SupabaseService service = new SupabaseService();
                var response = await service.LoginUser(user, _options.Value.Url,_options.Value.Key);
                if (!response.status)
                {
                    return  new BaseResponse<PatientAuthResponse<Doctor>>{ status = response.status, message = $"supabase {response.message}", StatusCode = StatusCodes.DatabaseError};
                }
                //Update MongoDB
                var userId = response.data.User.Id;
                
                Log.Information("Supabase User "+response.ToJson());
                user.password = string.Empty;
                var mongodbResponse = await  _accountRepository.GetUserById(userId);
                
                var otp = OtpHelper.GenerateNewOTP(5);
                
                var message = new ClickATellMessage
                {
                    to = mongodbResponse.data.PatientContactDetails.CellphoneNumber,
                    channel = "sms",
                    content = $"Serapis Medical: \n Your OTP is {otp}"
                };

                var  messaging = new Messaging()
                {
                    messages = new List<ClickATellMessage>()
                };
                messaging.messages.Add(message);
                bool isMessageSuccessful = await _messagingRepository.SendSms(messaging);
                Log.Information($"Was OTP: {otp} sent out to {message.to} ? : {isMessageSuccessful} ");
                return new BaseResponse<PatientAuthResponse<Doctor>>()
                {
                    status = true,
                    StatusCode = StatusCodes.Successful,
                    message = "Success on Supabase and Mongodb ",
                    data = new PatientAuthResponse<Doctor>
                    {
                        //Data = mongodbResponse.data,
                        SupabaseData = response.data,
                        Otp = otp
                    }
                };
            }
            catch (Exception e)
            {
                Log.Warning("LoginSSOUser:  {0}",JsonConvert.SerializeObject(e));
                return new BaseResponse<PatientAuthResponse<Doctor>>()
                    {message = e.ToString(), status = false, data = null, StatusCode = StatusCodes.FatalError};
            }
        }

        public async Task<BaseResponse<Doctor>> RegisterSSOUser(Doctor patient)
        {
            throw new NotImplementedException();
        }

        public async Task EditUser(Patient userwithToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponse<Patient>> AddAccount(Patient user)
        {
            throw new System.NotImplementedException();
        }

        public Patient GetUserById(string privateid)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateUser(Patient doctor)
        {
            throw new System.NotImplementedException();
        }
    }
}