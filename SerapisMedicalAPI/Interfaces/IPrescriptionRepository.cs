using SerapisMedicalAPI.Model.DoctorModel;
using System.Collections.Generic;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IPrescriptionRepository
    {
        //Get the prescription from mongodb --database doctor , patients and pharamcies use this method
        IEnumerable<DoctorPrescription> GetDocPrescription(int medId, int doctorId);

        //Post the prescription to mongoDb --Doctor app only 
        IEnumerable<DoctorPrescription> PrescribeMedication();

        //Put -only a doctor can edit the prescription (going to add this feature later on)
        DoctorPrescription EditPrescription(int doctorId);

        //Delete- only the same doctor can delete a prescription 
        DoctorPrescription RemovePrescription(int doctorId);
    }
}
