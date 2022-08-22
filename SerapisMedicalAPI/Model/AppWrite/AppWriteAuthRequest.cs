using Newtonsoft.Json;

namespace SerapisMedicalAPI.Model.AppWrite
{
    public class AppWriteAuthRequest
    {
        public string Email { get;  set; }
        public string Phone { get;  set; }
        public string Password  { get;  set; }
        public string Name { get;  set; }
    }
    
    public class AppWriteAuthResponse
    {
        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("$createdAt")]
        public long CreatedAt { get; set; }

        [JsonProperty("$updatedAt")]
        public long UpdatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("registration")]
        public long Registration { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("passwordUpdate")]
        public long PasswordUpdate { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("emailVerification")]
        public bool EmailVerification { get; set; }

        [JsonProperty("phoneVerification")]
        public bool PhoneVerification { get; set; }

        [JsonProperty("prefs")]
        public object Prefs { get; set; }
        
    }
}