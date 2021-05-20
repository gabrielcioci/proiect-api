using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using MongoDB.Driver;

namespace backend_II
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _users;

        public UsersService(ISamsariiDatabaseSettings settings)
        {
            var user = new MongoClient(settings.ConnectionString);
            var database = user.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(c => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _users.Find<User>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task UpdateAsync(string id, User user)
        {
            await _users.ReplaceOneAsync(c => c.Id == id, user);
        }

        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(c => c.Id == id);
        }
    }
}
