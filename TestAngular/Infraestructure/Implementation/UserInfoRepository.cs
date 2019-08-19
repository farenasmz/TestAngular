using System.Linq;
using System.Threading.Tasks;
using Infraestructure.GenericRepository;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Implementation
{
    public class UserInfoRepository : GenericRepository<UserInfo>, IUserRepository
    {
        private readonly DataContext context;
        public UserInfoRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public bool ValidateEmail(string email)
        {
            bool exist = false;
            UserInfo tmpUser = context.Users.Where(user => user.Email == email).FirstOrDefault();

            if (tmpUser != null)
            {
                exist = true;
            }

            return exist;
        }
        
        public Task<UserInfo> ValidateEmailAndPassword(string email, string password)
        {
            return context.Users.Where(user => user.Email == email && user.Password == password).FirstOrDefaultAsync();
        }
    }
}
