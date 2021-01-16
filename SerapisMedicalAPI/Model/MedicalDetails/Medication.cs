using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.MedicalDetails
{
    public class Medication
    {
        public string DrugCode { get; set; }
        public string Dosage { get; set; }
        public string MedicationName { get; set; }
        public string BrandName { get; set; }
        public MedicationType MedicationTypeValue { get; set; }
        
    }
}
