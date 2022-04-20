using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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
using Supabase.Gotrue;

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
        public async Task Test()
        {
            /*var theList = new List<Symptoms>();
            theList.Add( new Symptoms(){ID = "15", Name = "Name1"} );
            theList.Add( new Symptoms(){ID = "25", Name = "Name2"} );
            theList.Add( new Symptoms(){ID = "35", Name = "Name3"} );
            theList.Add( new Symptoms(){ID = "45", Name = "Name4"} );

            symptomCheckerRepository.PopulateSymptoms(theList);*/
            
            
            try
            {
                var url = "https://cgwlfswslvrkhvrrxckz.supabase.co";
                var key =
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNnd2xmc3dzbHZya2h2cnJ4Y2t6Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NTAyMTMwOTEsImV4cCI6MTk2NTc4OTA5MX0.mxmjA8BPQMJionCykKdtwfE8cuvqI0mnBFcVC4ADARQ";
                await Supabase.Client.InitializeAsync(url, key);
                var instance = Supabase.Client.Instance;

                var x1 = await instance.Auth.SignUp(Client.SignUpType.Phone,"27817004798", "password");
                var x = await instance.Auth.SignIn(Client.SignInType.Phone,"27817004798", "password");
                Debug.WriteLine(x.User.ToJson());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
