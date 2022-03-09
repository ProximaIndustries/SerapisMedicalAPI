using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace SerapisMedicalAPI.Helpers
{
    public class AuthHelper
    {
        // private object GenerateJWTToken(IEnumerable<Claim> claims, double timeInMinutes)
        // {
        //     TokenResponse response = new TokenResponse();
        //     try
        //     {
        //
        //         string secret = _jwtConfig.Value.TokenKey;
        //         var tokenHandler = new JwtSecurityTokenHandler();
        //         var key = CoreConstants.Encoding.ASCII.GetBytes(secret);
        //         var tokenDescriptor = new SecurityTokenDescriptor
        //         {
        //
        //             Subject = new ClaimsIdentity(claims),
        //             Issuer = _jwtConfig.Value.TokenIssuer,
        //             Audience = _jwtConfig.Value.TokenIssuer,
        //             Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt64(timeInMinutes)),
        //             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //                 SecurityAlgorithms.HmacSha256Signature)
        //         };
        //         var token = tokenHandler.CreateToken(tokenDescriptor);
        //         response.access_token = tokenHandler.WriteToken(token);
        //         response.expires_in = ((timeInMinutes * 60) - 1).ToString(CultureInfo.CurrentCulture);
        //         response.token_type = "Bearer";
        //     }
        //     catch (Exception)
        //     {
        //
        //     }
        //     return response;
        // }
        
        public string GetClaimValue(string type, HttpRequest request)
        {
            StringValues token = request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(token.FirstOrDefault()))
            {
                return string.Empty;
            }
            var securityToken = GetClaims(token);
            if (securityToken != null) return securityToken.Claims.FirstOrDefault(e => e.Type.ToLower() == type.ToLower()).Value;
            return string.Empty;
        }

        public JwtSecurityToken GetClaims(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {

                if (token.StartsWith("B"))
                {
                    token = token.Split(" ")[1];
                }
                var handler = new JwtSecurityTokenHandler();

                var decodedToken = handler.ReadToken(token) as JwtSecurityToken;

                return decodedToken;
            }
            return null;
        }

        public string GenerateJWTTokenV2()
        {
            // Creating JWT token
             var tokenHandler = new JwtSecurityTokenHandler();
             //Encodes a secret key
            
             var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authnetication");
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
             return tokenHandler.WriteToken(token);

             
        }
        
    }
}