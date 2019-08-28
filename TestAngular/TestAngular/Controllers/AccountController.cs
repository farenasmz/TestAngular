using Core.Users;
using Infraestructure.Dto;
using Infraestructure.GenericRepository;
using Infraestructure.Helpers;
using Infraestructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
			this.Configuration = iConfiguration;
			UserBusiness = new UsersBussiness(repository, logRepository);
		}

		[HttpGet]
		public IActionResult GetUsers()
		{
			return this.Ok(UserBusiness.GetAllUsers());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser([FromRoute] int id)
		{
			try
			{
				return await this.UserBusiness.GetUserById(id);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[Route("Create")]
		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] User model)
		{
			jwtDto result;

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
				catch (Exception)
				{
					return BadRequest();
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
				return Ok(await UserBusiness.Login(User, Configuration["SuperKey"]));
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

        public async Task<IActionResult> PutUser([FromBody] User user)
        {
            User tmpUser;

            try
            {
                if (user == null)
                {
                    return NotFound();
                }

				await UserBusiness.UpdateUser(user);
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
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
			catch (Exception)
			{
				return BadRequest();
			}
		}
	}
}
