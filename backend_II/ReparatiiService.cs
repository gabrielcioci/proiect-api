using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_II
{
    public class ReparatiiService
    {
        private readonly IMongoCollection<Reparatie> reparatii;

        public ReparatiiService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("reprezentanta"));
            var database = client.GetDatabase("reprezentanta");
            reparatii = database.GetCollection<Reparatie>("reparatii");
        }

        public async Task<List<Reparatie>> GetAllAsync()
        {
            return await reparatii.Find(c => true).ToListAsync();
        }

        public async Task<Reparatie> GetByIdAsync(string id)
        {
            return await reparatii.Find<Reparatie>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Reparatie>> GetByUserId(string id)
        {
            return await reparatii.Find(c => c.clientID == id).ToListAsync();
        }


        public async Task<Reparatie> CreateAsync(Reparatie reparatie)
        {
            await reparatii.InsertOneAsync(reparatie);
            return reparatie;
        }

        public async Task UpdateAsync(string id, Reparatie reparatie)
        {
            await reparatii.ReplaceOneAsync(c => c.Id == id, reparatie);
        }

        public async Task DeleteAsync(string id)
        {
            await reparatii.DeleteOneAsync(c => c.Id == id);
        }
    }
}
