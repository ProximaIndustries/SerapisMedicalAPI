using Microsoft.Extensions.Options;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerapisMedicalAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using MongoDB.Driver;

namespace SerapisMedicalAPI.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Context _context = null;

        public PatientUser user = new PatientUser();

        public AccountRepository()
        {
            _context = new Context();
            // _appSettings = appSettings.Value;
        }
        public PatientUser RegisterandAuthenticateAsync(PatientUser user)
        {

             
            // Creating JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            //Encodes a secret key
           
            var key = Encoding.ASCII.GetBytes(" ");
            //user.PrivateId = 1;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString() )
                }),
                //sets token expiry date to 7 days
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            
            _context.PatientCollection.InsertOne(user);
           
            // remove password before returning
            user.Password = null;
            return user;
        }

        public async Task<IEnumerable<PatientUser>> GetAllRegisteredUsers()
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
        public async Task RegisterSocialUser(PatientUser patient)
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
        
        public async Task<PatientUser> FacebookLogin(PatientUser patient)
        {

            //check if user exists
            try
            {
                var filter = Builders<PatientUser>
                .Filter
                .Eq(user => user.SocialId, patient.SocialId);

                var RegisteredUser = await _context.PatientCollection
                                                   .Find(filter)
                                                   .FirstOrDefaultAsync();
                //if user exists login him in 
                //else register the user and sign him in
                if (RegisteredUser == null)
                {
                    //we need to add a field that keeps record if this is a new user or not.
                    await _context.PatientCollection
                                  .InsertOneAsync(patient);                                                                                                                                                                                                                                                                                         
                    
                }
                else
                {
                    //user already exists so return user
                    return RegisteredUser; //needs to return a success method rather
                }
                return patient;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
                //add notification service
            }
           
        }


        public async Task EditUser(PatientUser userwithToken)
        {
            ReplaceOneResult updateResult =
                    await _context
            .PatientCollection
            .ReplaceOneAsync(
                filter: g => g.PrivateId == userwithToken.PrivateId,
                replacement: userwithToken);
        }

        public async Task AddAccount(PatientUser user)
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

       
        public Task<PatientUser> GetUser(string privateid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(PatientUser doctor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditUser(Doctor doctor)
        {
            throw new NotImplementedException();
        }

       
    }
}
