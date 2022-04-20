﻿using System.Threading.Tasks;
using SerapisMedicalAPI.Data.Base;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.PatientModel;
using SerapisMedicalAPI.Model.Supabase;

namespace SerapisMedicalAPI.Services.SymptomsChecker
{
    public interface IAccountSupabaseRepository
    {
         Task EditUser(Patient userwithToken);
         Task<BaseResponse<Patient>> AddAccount(Patient user); //For Admin access
         
         Task<BaseResponse<Patient>> RegisterUser(Patient patient);
         Task<BaseResponse<PatientAuthResponse>> LoginUser(SupabaseAuth user);
         public Patient GetUserById(string privateid);
         Task<bool> UpdateUser(Patient doctor);
    }
}