using Infraestructure.GenericRepository;
using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Models
{
    public class UserInfo : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool isActive { get; set; }
    }
}
