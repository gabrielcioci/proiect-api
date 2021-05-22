using System;
namespace backend_II.Models
{
    public class SamsariiDatabaseSettings : ISamsariiDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ReparatiiCollectionName { get; set; }
        public string ClientiCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISamsariiDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string ReparatiiCollectionName { get; set; }
        string ClientiCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
