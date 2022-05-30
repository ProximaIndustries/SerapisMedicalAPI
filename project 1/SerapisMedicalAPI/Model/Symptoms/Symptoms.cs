using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.Symptoms
{
    public class Symptoms
    {
        //[JsonPropertyName("addressLineone")]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
