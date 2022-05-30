using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Postgrest.Responses;
using SerapisMedicalAPI.Data.Base;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.PatientModel;
using SerapisMedicalAPI.Model.Supabase;
using SerapisMedicalAPI.Model.Symptoms;
using Serilog;
using Supabase;
using Supabase.Gotrue;
using Client = Supabase.Gotrue.Client;

namespace SerapisMedicalAPI.Services
{
    public class SupabaseService
    {

        public async Task<BaseResponse<Session>> RegisterUser(Patient patient,string url, string key)
        {
            try
            {
                await Supabase.Client.InitializeAsync(url, key);
                var instance = Supabase.Client.Instance;

                var session = await instance.Auth.SignUp(Client.SignUpType.Phone,
                    patient.PatientContactDetails.CellphoneNumber, patient.Token);

                return new BaseResponse<Session> {data = session, status = true, StatusCode = "200"};
            }
            catch (BadRequestException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.BadRequest && ex.Response.ReasonPhrase == "Bad Request")
                {
                    //User already registered
                    Log.Error($"User already exists {ex.Content}");
                    return new BaseResponse<Session> {StatusCode = StatusCodes.AuthenticonError, status = false, message = "User already registered on supabase "};
                }      
                Log.Error(ex.Content);
                return new BaseResponse<Session> {StatusCode = "0", status = false, message = $"supabase has issues {ex.ToString()}"};
            }
            catch (Exception e){
                
                Log.Error(e.ToString());
                return new BaseResponse<Session> {StatusCode = "0", status = false, message = $"supabase has issues {e.ToString()}"};
            }
        }

        public async Task<BaseResponse<Session>> LoginUser(SupabaseAuth patient,string url, string key)
        {
            try
            {
                await Supabase.Client.InitializeAsync(url, key);
                var instance = Supabase.Client.Instance;
                
                var Session = await instance.Auth.SignIn(Client.SignInType.Phone,patient.phone,patient.password );
                
                return new BaseResponse<Session>{ data = Session, status = true, StatusCode = "200", message = ""};
            }
            catch (BadRequestException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.BadRequest)
                {
                    //User already registered
                    Log.Error($"User already exists {ex.Content}");
                    return new BaseResponse<Session> {StatusCode = StatusCodes.AuthenticonError, status = false, message = $"supabase has issues {ex.Content.ToString()} "};
                }      
                Log.Error(ex.Content);
                return new BaseResponse<Session> {StatusCode = "0", status = false, message = $"supabase has issues {ex.Content.ToString()}"};
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return new BaseResponse<Session> {StatusCode = StatusCodes.DatabaseError, status = false, data = null, message = e.Message};
            }
        }
        
        public async Task<BaseResponse<Session>> DeleteUser(Patient patient,string url, string key)
        {
            try
            {
                await Supabase.Client.InitializeAsync(url, key);
                var instance = Supabase.Client.Instance;
                
                var Session = await instance.Auth.DeleteUser("","key");
                
                return new BaseResponse<Session>{ status = Session, StatusCode = "200"};
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse<Session> {StatusCode = "0", status = false, data = null};
            }
        }
    }
}