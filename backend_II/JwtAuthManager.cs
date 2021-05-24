using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_II.Models;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace backend_II
{
    public class JwtAuthManager : IJwtAuthManager

    {
        private readonly string key;
        private readonly IMongoCollection<User> users;

        public JwtAuthManager(string key)
        {
            this.key = key;
        }

        public string Authenticate(string email, string password)
        {
            var user = users.Find(u => u.email == email && u.password == password).FirstOrDefault();
            if (user == null) 
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new(new Claim[]
                {
                    new Claim(ClaimTypes.Email,email)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
