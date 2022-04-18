using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SerapisMedicalAPI;
using SerapisMedicalAPI.Data;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Services.SymptomsChecker;
using Moq;
using SerapisMedicalAPI.Enums;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.Symptoms;

namespace SerapisMedical.Test
{
    
    public class Class1
    {
        private CassandraContext context;
        private ISymptomCheckerRepository symptomCheckerRepository;
        private ISymptomsCheckerService symptomsCheckerService;

        /*public Class1(ISymptomCheckerRepository _SymptomCheckerRepository)
        {
            symptomCheckerRepository = _SymptomCheckerRepository;
        }*/
        
        [SetUp]
        public void Setup()
        {
            //
            //SymptomCheckerRepository = new Mock<ISymptomCheckerRepository>();
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            var value = (Genders) 1;
            /*AuthHelper authHelper = new AuthHelper();
            authHelper.GenerateJWTTokenV2();*/
        
            Console.WriteLine(value.ToString());
           Console.WriteLine(Genders.Female.ToString());

        }
        
        [Test]
        public void Test()
        {
            var theList = new List<Symptoms>();
            theList.Add( new Symptoms(){ID = "15", Name = "Name1"} );
            theList.Add( new Symptoms(){ID = "25", Name = "Name2"} );
            theList.Add( new Symptoms(){ID = "35", Name = "Name3"} );
            theList.Add( new Symptoms(){ID = "45", Name = "Name4"} );

            symptomCheckerRepository.PopulateSymptoms(theList);

        }
    }
}
