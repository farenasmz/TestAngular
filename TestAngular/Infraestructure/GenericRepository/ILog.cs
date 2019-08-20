using Infraestructure.Models;
using System.Threading.Tasks;

namespace Infraestructure.GenericRepository
{
	public interface ILog:  IGenericRepository<Log>
	{
		Task CreateLogAsync(User user, string description);
	}
}
