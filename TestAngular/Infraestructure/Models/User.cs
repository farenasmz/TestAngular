using Infraestructure.GenericRepository;
using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Models
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
    }
}
