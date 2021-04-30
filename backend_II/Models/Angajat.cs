﻿using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_II.Models
{
    public class Angajat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Nume is required")]
        public string nume { get; set; }
        [Required(ErrorMessage = "Prenume is required")]
        public string prenume { get; set; }
        [Required(ErrorMessage = "CNP is required")]
        public string CNP { get; set; }
        [Required(ErrorMessage = "Telefon is required")]
        public string telefon { get; set; }
        [Required(ErrorMessage = "Post is required")]
        public string post { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}