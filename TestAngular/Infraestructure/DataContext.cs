using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<BookProduct> BookProducts { get; set; }
		public DbSet<Log> Logs { get; set; }
	}
}
