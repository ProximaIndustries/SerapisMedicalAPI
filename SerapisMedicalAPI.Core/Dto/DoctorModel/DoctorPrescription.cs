using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Core.Dto.DoctorModel
{
    public class DoctorPrescription
    {
        public ObjectId Id { get; set; }
        public int MedsInBusket { get; set; }
        public string PrescriptionDosage { get; set; }
        public string PrescriptionInstructions { get; set; }
        public string PrescriptionMedication { get; set; }
        public string AddedPrescriptionNotes { get; set; }
        public double MedCashPrice { get; set; }
        public int ItemNum { get; set; }
        public MedicationType TypeOfMedication { get; set; }
    }
}
