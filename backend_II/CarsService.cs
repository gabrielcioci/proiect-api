using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using MongoDB.Driver;

namespace backend_II
{
    public class CarsService
    {
        private readonly IMongoCollection<Car> _cars;

        public CarsService(ISamsariiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cars = database.GetCollection<Car>(settings.CarsCollectionName);
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _cars.Find(c => true).ToListAsync();
        }

        public async Task<Car> GetByIdAsync(string id)
        {
            return await _cars.Find<Car>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Car> CreateAsync(Car car)
        {
            await _cars.InsertOneAsync(car);
            return car;
        }

        public async Task UpdateAsync(string id, Car car)
        {
            await _cars.ReplaceOneAsync(c => c.Id == id, car);
        }

        public async Task DeleteAsync(string id)
        {
            await _cars.DeleteOneAsync(c => c.Id == id);
        }
    }
}
