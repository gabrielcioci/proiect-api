﻿using System;
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
        public string model { get; set; }
        public string culoare { get; set; }
        [Required(ErrorMessage = "An is required")]
        public int an { get; set; }
        [Required(ErrorMessage = "Pret is required")]
        public int pret { get; set; }
        public Boolean hybrid { get; set; }
        [Required(ErrorMessage = "CP is required")]
        public int CP { get; set; }
    }
}