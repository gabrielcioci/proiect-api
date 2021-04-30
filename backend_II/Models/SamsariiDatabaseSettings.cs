using System;
namespace backend_II.Models
{
    public class SamsariiDatabaseSettings : ISamsariiDatabaseSettings
    {
        public string CarsCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string ReparatiiCollectionName { get; set; }
        public string AngajatiCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISamsariiDatabaseSettings
    {
        string CarsCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string ReparatiiCollectionName { get; set; }
        string AngajatiCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
