using System;
namespace SerapisMedicalAPI.Helpers.Validations
{
    public class DoctorValidation
    {
        public bool isValidMPNumber(string MPID){
                
            string MP = MPID.Substring(0,1);
            if(MP.ToLower() != "mp")
                return false;

            return true;
        }
    }
}