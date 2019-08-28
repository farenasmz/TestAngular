using Infraestructure.GenericRepository;
using Infraestructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logs
{
	public class LogsBussiness
	{
		private readonly ILog Repository;

		public LogsBussiness(ILog repository)
		{
			Repository = repository;
		}

		public List<Log> GetProducts()
		{
			return Repository.GetAll().ToList();
		}
	}
}
