using Infraestructure.GenericRepository;
using Infraestructure.Models;
using System;
using System.Threading.Tasks;

namespace Infraestructure.Implementation
{
	public class LogRepository : GenericRepository<Log>, ILog
	{
		public LogRepository(DataContext context) : base(context)
		{
		}

		public async Task CreateLogAsync(User user, string description)
		{
            Log log = new Log
            {
                UserName = user.Email,
                Description = description,
                DateLogged = DateTime.UtcNow.AddHours(-5)
            };

            await this.CreateAsync(log);
		}
	}
}
