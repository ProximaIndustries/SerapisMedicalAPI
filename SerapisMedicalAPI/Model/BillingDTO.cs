using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model
{
    public class BillingDTO
    {
        public string merchant_id { get; set; }
        public string merchant_key { get; set; }

        public string amount { get; set; }
        public string item_name { get; set; }
    }
    
    public class BuyerDetails
    {
        public string name_first { get; set; }

        public string name_surname { get; set; }

        public string email_address { get; set; }

        public string cell_number { get; set; }
    }

    public class TransactionDetails
    {
        public string item_description { get; set; }

        public string m_payment_id { get; set; }

        public string amount { get; set; }

        public string item_name { get; set; }
    }
}
