using System;
namespace backend_II.Models
{
    public class SamsariiDatabaseSettings : ISamsariiDatabaseSettings
    {
        public string CarsCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISamsariiDatabaseSettings
    {
        string CarsCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
