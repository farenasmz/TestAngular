using Infraestructure.GenericRepository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Models
{
	public class Log : IEntity
	{
		[Key]
		public int Id { get; set; }
		public string UserName { get; set; }
		public DateTime DateLogged { get; set; }
		public string Description { get; set; }
	}
}
