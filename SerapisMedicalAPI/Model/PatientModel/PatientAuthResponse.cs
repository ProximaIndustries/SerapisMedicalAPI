using Supabase.Gotrue;

namespace SerapisMedicalAPI.Model.PatientModel
{
    public class PatientAuthResponse<TObject> where TObject : class 
    {
        public TObject Data { get; set; }
        public Session SupabaseData { get; set; }
        public string Otp { get; set; }
    }
}