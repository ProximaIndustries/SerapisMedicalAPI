using SerapisMedicalAPI.Model.Symptoms;

namespace SerapisMedicalAPI.Interfaces
{
    public interface ISymptomCheckerRepository
    {

        Symptoms GetSymptomById();
    }
}