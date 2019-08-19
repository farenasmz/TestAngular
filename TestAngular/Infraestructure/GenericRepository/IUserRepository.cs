using Infraestructure.Models;
using System.Threading.Tasks;

namespace Infraestructure.GenericRepository
{
    public interface IUserRepository : IGenericRepository<UserInfo>
    {
        Task<UserInfo> ValidateEmailAndPassword(string email, string password);
    }
}
