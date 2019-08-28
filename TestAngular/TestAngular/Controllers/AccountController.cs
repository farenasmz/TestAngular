using Core.Users;
using Infraestructure.Dto;
using Infraestructure.GenericRepository;
using Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace TestAngular.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IConfiguration Configuration;
		private readonly UsersBussiness UserBusiness;

		public AccountController(IUserRepository repository, IConfiguration iConfiguration, ILog logRepository)
		{
			Configuration = iConfiguration;
			UserBusiness = new UsersBussiness(repository, logRepository);
		}

		[HttpGet]
		public IActionResult GetUsers()
		{
			return Ok(UserBusiness.GetAllUsers());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser([FromRoute] int id)
		{
			try
			{
				return await UserBusiness.GetUserById(id);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("Create")]
		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] User model)
		{
			SecurityLoginDto result;

			if (ModelState.IsValid)
			{
				try
				{
					result = await UserBusiness.CreateUser(model, Configuration["SuperKey"]);

					if (result == null)
					{
						return BadRequest("Email already registered.");
					}

					return Ok(result);
				}
				catch (Exception ex)
				{
					return BadRequest(ex);
				}
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] User User)
		{
			if (ModelState.IsValid)
			{
				try
				{

				return Ok(await UserBusiness.Login(User, Configuration["SuperKey"]));

				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		public async Task<IActionResult> PutUser([FromBody] User user)
		{
			try
			{
				if (user == null)
				{
					return NotFound();
				}

				await UserBusiness.UpdateUser(user);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Product>> DeleteUser(int id)
		{
			try
			{
				await UserBusiness.DisableUser(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
