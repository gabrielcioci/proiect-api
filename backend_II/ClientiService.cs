using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_II
{
    public class ClientiService
    {
        private readonly IMongoCollection<Client> clienti;

        public ClientiService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("reprezentanta"));
            var database = client.GetDatabase("reprezentanta");
            clienti = database.GetCollection<Client>("clienti");
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await clienti.Find(c => true).ToListAsync();
        }

        public async Task<Client> GetByIdAsync(string id)
        {
            return await clienti.Find<Client>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Client> CreateAsync(Client client)
        {
            await clienti.InsertOneAsync(client);
            return client;
        }

        public async Task UpdateAsync(string id, Client client)
        {
            await clienti.ReplaceOneAsync(c => c.Id == id, client);
        }

        public async Task DeleteAsync(string id)
        {
            await clienti.DeleteOneAsync(c => c.Id == id);
        }
    }
}
