using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using MongoDB.Driver;

namespace backend_II
{
    public class AngajatiService
    {
        private readonly IMongoCollection<Angajat> _angajati;

        public AngajatiService(ISamsariiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _angajati = database.GetCollection<Angajat>(settings.AngajatiCollectionName);
        }

        public async Task<List<Angajat>> GetAllAsync()
        {
            return await _angajati.Find(c => true).ToListAsync();
        }

        public async Task<Angajat> GetByIdAsync(string id)
        {
            return await _angajati.Find<Angajat>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Angajat> CreateAsync(Angajat angajat)
        {
            await _angajati.InsertOneAsync(angajat);
            return angajat;
        }

        public async Task UpdateAsync(string id, Angajat angajat)
        {
            await _angajati.ReplaceOneAsync(c => c.Id == id, angajat);
        }

        public async Task DeleteAsync(string id)
        {
            await _angajati.DeleteOneAsync(c => c.Id == id);
        }
    }
}
