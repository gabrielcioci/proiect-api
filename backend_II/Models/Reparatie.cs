using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_II.Models
{
    public class Reparatie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Model is required")]
        public string model { get; set; }
        [Required(ErrorMessage = "An is required")]
        public int an { get; set; }
        [Required(ErrorMessage = "Cost is required")]
        public int cost { get; set; }
        [Required(ErrorMessage = "Reparat is required")]
        public Boolean reparat { get; set; }
        public string detalii { get; set; }
    }
}
