using Infraestructure.GenericRepository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Models
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }
              
        public string Description { get; set; }

        public Int32 Quantity { get; set; }

		public Int32 AvailableQuantity { get; set; }

		public bool IsActive { get; set; }
    }
}
