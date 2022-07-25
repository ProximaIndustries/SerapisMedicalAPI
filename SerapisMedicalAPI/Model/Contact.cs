﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class Contact
    {
        public string Email { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        public string Enquiry_Topic { get; set; }

        public string Message { get; set; }
    }
}
