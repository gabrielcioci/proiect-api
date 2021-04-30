using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using MongoDB.Driver;

namespace backend_II
{
    public class ReparatiiService
    {
        private readonly IMongoCollection<Reparatie> _reparatii;

        public ReparatiiService(ISamsariiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _reparatii = database.GetCollection<Reparatie>(settings.ReparatiiCollectionName);
        }

        public async Task<List<Reparatie>> GetAllAsync()
        {
            return await _reparatii.Find(c => true).ToListAsync();
        }

        public async Task<Reparatie> GetByIdAsync(string id)
        {
            return await _reparatii.Find<Reparatie>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Reparatie> CreateAsync(Reparatie reparatie)
        {
            await _reparatii.InsertOneAsync(reparatie);
            return reparatie;
        }

        public async Task UpdateAsync(string id, Reparatie reparatie)
        {
            await _reparatii.ReplaceOneAsync(c => c.Id == id, reparatie);
        }

        public async Task DeleteAsync(string id)
        {
            await _reparatii.DeleteOneAsync(c => c.Id == id);
        }
    }
}
