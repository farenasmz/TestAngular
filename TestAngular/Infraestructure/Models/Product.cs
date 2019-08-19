﻿using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
