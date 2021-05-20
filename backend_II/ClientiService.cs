using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using MongoDB.Driver;

namespace backend_II
{
    public class ClientiService
    {
        private readonly IMongoCollection<Client> _clienti;

        public ClientiService(ISamsariiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _clienti = database.GetCollection<Client>(settings.ClientiCollectionName);
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _clienti.Find(c => true).ToListAsync();
        }

        public async Task<Client> GetByIdAsync(string id)
        {
            return await _clienti.Find<Client>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Client> CreateAsync(Client client)
        {
            await _clienti.InsertOneAsync(client);
            return client;
        }

        public async Task UpdateAsync(string id, Client client)
        {
            await _clienti.ReplaceOneAsync(c => c.Id == id, client);
        }

        public async Task DeleteAsync(string id)
        {
            await _clienti.DeleteOneAsync(c => c.Id == id);
        }
    }
}
