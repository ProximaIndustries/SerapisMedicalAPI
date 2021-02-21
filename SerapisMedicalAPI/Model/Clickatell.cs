using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class Clickatell
    {
        public IList<Message> messages { get; set; }
    }

    public class Message
    {
        public string channel { get; set; }
        public string to { get; set; }
        public string content { get; set; }
    }
}
