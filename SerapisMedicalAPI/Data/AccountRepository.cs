using Microsoft.Extensions.Options;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerapisMedicalAPI.Interfaces;
using System.Text;
using System.Security.Claims;
using MongoDB.Driver;
using SerapisMedicalAPI.Model.PatientModel;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;

namespace SerapisMedicalAPI.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Context _context;
        private readonly ILogger<AccountRepository> _logger;

        public Patient user = new Patient();

        public AccountRepository(Context context)
        {
            _context = context;
            // _appSettings = appSettings.Value;
        }
        public Patient RegisterandAuthenticateAsync(Patient user)
        {

             
            // Creating JWT token
           /* var tokenHandler = new JwtSecurityTokenHandler();
            //Encodes a secret key
           
            var key = Encoding.ASCII.GetBytes(" ");
            //user.PrivateId = 1;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                   // new Claim(ClaimTypes.Name, user.Id.ToString() )
                }),
                //sets token expiry date to 7 days
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);*/

            
            _context.PatientCollection.InsertOne(user);
           
            // remove password before returning
            //user.Password = null;
            return user;
        }

        public async Task<IEnumerable<Patient>> GetAllRegisteredUsers()
        {
            try
            {
                
                var result = await _context.PatientCollection
                    .Find(_ => true)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

        }
        
        /// <summary>
        /// Register
        /// </summary>
        /// <returns></returns>
        public async Task RegisterSocialUser(Patient patient)
       {
            try
            {
                await _context
                    .PatientCollection
                    .InsertOneAsync(patient);
            }
            catch (Exception ex)
            {
                throw ex;
            }  
       }
        
        //In Production
        public async Task<Patient> SocialLogin(string socialid, string firstname, string lastname)
        {
            //check if user exists
            try
            {

                //if facebook || google

                var filter = Builders<Patient>
                .Filter
                .Eq(user => user.SocialID, socialid);

                var RegisteredUser = await _context?.PatientCollection
                                                   .Find(filter)
                                                   .FirstOrDefaultAsync();
                //if user exists login him in 
                //else register the user and sign him in
                if (RegisteredUser == null)
                {
                    Patient patient = new Patient
                    {
                        SocialID = socialid,
                        PatientFirstName = firstname,
                        PatientLastName = lastname
                    };
                    //we need to add a field that keeps record if this is a new user or not.
                    await _context.PatientCollection
                                  .InsertOneAsync(patient);
                    return patient;   
                }
                return RegisteredUser;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
                //add notification service
            }
           
        }
        public async Task EditUser(Patient userwithToken)
        {
            ReplaceOneResult updateResult =
                    await _context
            .PatientCollection
            .ReplaceOneAsync(
                filter: g => g.id == userwithToken.id,
                replacement: userwithToken);
        }

        public async Task AddAccount(Patient user)
        {
            try
            {
                await _context
               .PatientCollection
               .InsertOneAsync(user);
            }
            catch (Exception ex)
            {

            }
           
        }

       
        public Patient GetUserById(string privateid)
        {
            Patient _patient = new Patient();
            //_patient.id = ObjectId.Parse(privateid);
            var filter = Builders<Patient>.Filter.Eq(user => user.id, _patient.id);
            try
            {
                Patient user =  _context.PatientCollection.Find(filter).SingleOrDefault();

                return user;
                
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public Task<bool> UpdateUser(Patient doctor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditUser(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        PatientUser IAccountRepository.RegisterandAuthenticateAsync(Patient user)
        {
            throw new NotImplementedException();
        }
    }
}
