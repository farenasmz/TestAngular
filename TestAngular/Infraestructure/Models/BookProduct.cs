using Infraestructure.GenericRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infraestructure.Models
{
    public class BookProduct : IEntity
	{
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserID { get; set; }

		public int Quantity { get; set; }
		
		public Product Product { get; set; }
        public User User { get; set; }
    }
}
