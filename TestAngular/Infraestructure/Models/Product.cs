using Infraestructure.GenericRepository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Models
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product description")]
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "field must be atleast 6 characters")]
        public string Description { get; set; }

        [Display(Name = "Product Quantity")]
        [Required]
        public Int32 Quantity { get; set; }

        public bool IsActive { get; set; }
    }
}
