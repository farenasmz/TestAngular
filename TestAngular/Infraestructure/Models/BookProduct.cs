using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infraestructure.Models
{
    public class BookProduct
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserID { get; set; }

        public Product product { get; set; }
        public User user { get; set; }
    }
}
