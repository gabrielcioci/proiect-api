using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_II.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }
        public string Color { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }
        public Boolean Hybrid { get; set; }
        [Required(ErrorMessage = "Horsepower field is required")]
        public int Horsepower { get; set; }
        public Boolean Sold { get; set; }
    }
}
