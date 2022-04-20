using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
        
        public AccountSupabaseRepository(IAccountRepository accountRepository, IOptions<SupabaseConfig> options)
        {
            _accountRepository = accountRepository;
            _options = options;
        }
        public async Task<BaseResponse<Patient>> RegisterUser(Patient patient)
        {
            try
            {
                SupabaseService service = new SupabaseService();
                var response = await service.RegisterUser(patient, _options.Value.Url,_options.Value.Key);
                if (!response.status)
                {
                    return  new BaseResponse<Patient>() { status = false, message = "failed to register on supabase",StatusCode = "1"};
                }
                //Update MongoDB
                var userId = response.data.User.Id;
                patient.SocialID = userId;
                patient.Token = string.Empty;
                var mongodbResponse =  _accountRepository.AddAccount(patient);

                if (!mongodbResponse.status)
                {
                    //if mongodb update fails put it under a table called "Account-Reconcilition" 
                    return  new BaseResponse<Patient>() { status = false, message = "mongodb failed"};
                
                }

                return new BaseResponse<Patient>() { data = mongodbResponse.data, status = true, message = "mongo success"};
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return  new BaseResponse<Patient>() { status = false, message = $"something went wrong {e.ToString()}"};
            }
            
            
        }
        
        public async Task<BaseResponse<PatientAuthResponse>> LoginUser([FromBody] SupabaseAuth user)
        {
            try
            {
                SupabaseService service = new SupabaseService();
                var response = await service.LoginUser(user, _options.Value.Url,_options.Value.Key);
                if (!response.status)
                {
                    return  new BaseResponse<PatientAuthResponse>{ status = response.status, message = $"supabase {response.message}", StatusCode = StatusCodes.DatabaseError};
                }
                //Update MongoDB
                var userId = response.data.User.Id;
                
                Log.Information("Supabase User "+response.ToJson());
                user.password = string.Empty;
                var mongodbResponse = await  _accountRepository.GetUserById(userId);
                
                
                return new BaseResponse<PatientAuthResponse>()
                {
                    status = true,
                    StatusCode = StatusCodes.Successful,
                    data = new PatientAuthResponse(){ PatientData = mongodbResponse.data, SupabaseData = response.data}
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse<PatientAuthResponse>()
                    {message = e.ToString(), status = false, data = null, StatusCode = StatusCodes.FatalError};
            }
        }
        
        public async Task EditUser(Patient userwithToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BaseResponse<Patient>> AddAccount(Patient user)
        {
            throw new System.NotImplementedException();
        }

        public Patient GetUserById(string privateid)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateUser(Patient doctor)
        {
            throw new System.NotImplementedException();
        }
    }
}