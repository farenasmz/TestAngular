using System.Linq;
using System.Threading.Tasks;
using Infraestructure.GenericRepository;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Implementation
{
    public class UserInfoRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext context;
        public UserInfoRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public bool ValidateEmail(string email)
        {
            bool exist = false;
            User tmpUser = context.Users.Where(user => user.Email == email).FirstOrDefault();

            if (tmpUser != null)
            {
                exist = true;
            }

            return exist;
        }

        public Task<User> GetByEmail(string email)
        {
            return context.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
        }

        public Task<User> ValidateEmailAndPassword(string email, string password)
        {
            return context.Users.Where(user => user.Email == email && user.Password == password).FirstOrDefaultAsync();
        }
    }
}
