using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class ContactDTO
    {
        /*public string Email { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }*/

        public IList<Contact> Contacts { get; set; }
    }

    public class Contact
    {
        public string Email { get; set; }
        public string First_name { get; set; }
    }
}
