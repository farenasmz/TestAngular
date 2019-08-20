using Infraestructure.Models;
using System.Threading.Tasks;

namespace Infraestructure.GenericRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> ValidateEmailAndPassword(string email, string password);
        bool ValidateEmail(string email);
        Task<User> GetByEmail(string email);
    }
}
