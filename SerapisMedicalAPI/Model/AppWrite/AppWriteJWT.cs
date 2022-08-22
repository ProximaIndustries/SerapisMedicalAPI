using System.Text.Json.Serialization;

namespace SerapisMedicalAPI.Model.AppWrite
{
    public class AppWriteJWT
    {
        [JsonPropertyName("jwt")]
        public string JWT { get; private set; }
    }
}