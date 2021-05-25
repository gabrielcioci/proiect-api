using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace backend_II
{
    public class UsersService
    {
        private readonly IMongoCollection<User> users;
        private readonly string key;

        public UsersService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("reprezentanta"));
            var database = client.GetDatabase("reprezentanta");
            users = database.GetCollection<User>("users");
            this.key = configuration.GetSection("JwtKey").ToString();
        }

        public string Authenticate(string email,string password)
        {
            var user = users.Find(u => u.email == email && u.password == password).FirstOrDefault();
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email,email)
                }),

                Expires = DateTime.UtcNow.AddHours(12),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await users.Find(c => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await users.Find<User>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await users.Find<User>(c => c.email == email).FirstOrDefaultAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            await users.InsertOneAsync(user);
            return user;
        }

        public async Task UpdateAsync(string id, User user)
        {
            await users.ReplaceOneAsync(c => c.Id == id, user);
        }

        public async Task DeleteAsync(string id)
        {
            await users.DeleteOneAsync(c => c.Id == id);
        }
    }
}
