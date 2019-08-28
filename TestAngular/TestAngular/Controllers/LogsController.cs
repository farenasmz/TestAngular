using Core.Logs;
using Infraestructure.GenericRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestAngular.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class LogsController : ControllerBase
	{
		private readonly LogsBussiness LogsBussiness;

		public LogsController(ILog repository)
		{
			LogsBussiness = new LogsBussiness(repository);
		}

		[HttpGet]
		public IActionResult GetProducts()
		{
			return Ok(LogsBussiness.GetProducts());
		}
	}
}