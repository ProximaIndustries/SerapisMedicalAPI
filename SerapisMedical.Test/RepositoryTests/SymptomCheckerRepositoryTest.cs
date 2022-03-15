using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SerapisMedicalAPI.Data;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model.Symptoms;

namespace SerapisMedical.Test.RepositoryTests
{
    public class SymptomCheckerRepositoryTest
    {
        private ISymptomCheckerRepository _SymptomCheckerRepository;
    /*
        {
    symptomCheckerRepository = _SymptomCheckerRepository;
}*/
        private Mock<ISymptomCheckerRepository> _symptomCheckerRepositoryMock;

        [SetUp]
        public void Setup()
        {
            string id = "234-11-16";
            /*_symptomCheckerRepositoryMock = new Mock<ISymptomCheckerRepository>();
            _symptomCheckerRepositoryMock.Setup(x => x.GetProposedDiagnosisBySymptoms(id))
                .Returns(default(object));*/
            //SymptomCheckerRepository = new Mock<ISymptomCheckerRepository>();
        }
        
        [Test]
        public void PerformTest(ISymptomCheckerRepository symptomCheckerRepository)
        {
            _SymptomCheckerRepository = symptomCheckerRepository;

            IEnumerable<DiagnosisResponse> x = _SymptomCheckerRepository.GetProposedDiagnosisBySymptoms("234-11-16");
            
            Console.WriteLine(x);
        }
    }
}