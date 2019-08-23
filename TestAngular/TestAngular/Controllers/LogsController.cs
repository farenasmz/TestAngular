using Infraestructure.GenericRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestAngular.Controllers
{
	[Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class LogsController : ControllerBase
	{
		private readonly ILog Repository;

		public LogsController(ILog repository)
		{
			Repository = repository;
		}

		[HttpGet]
		public IActionResult GetProducts()
		{
			return Ok(Repository.GetAll());
		}
	}
}