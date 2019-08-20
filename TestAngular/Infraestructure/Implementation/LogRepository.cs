using Infraestructure.GenericRepository;
using Infraestructure.Models;
using System;
using System.Threading.Tasks;

namespace Infraestructure.Implementation
{
	public class LogRepository : GenericRepository<Log>, ILog
	{
		private readonly DataContext context;
		public LogRepository(DataContext context) : base(context)
		{
			this.context = context;
		}

		public async Task CreateLogAsync(User user, string description)
		{
			Log log = new Log();
			log.UserName = user.Email;
			log.Description = description;
			log.DateLogged = DateTime.UtcNow.AddHours(-5);
			await this.CreateAsync(log);
		}
	}
}
