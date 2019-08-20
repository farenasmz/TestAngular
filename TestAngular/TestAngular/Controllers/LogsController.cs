using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infraestructure.GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
		private readonly ILog Repository;

		public LogsController(ILog repository)
		{
			this.Repository = repository;
		}

		[HttpGet]
		public IActionResult GetProducts()
		{
			return this.Ok(this.Repository.GetAll());
		}
	}
}