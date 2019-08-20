using Infraestructure.GenericRepository;
using Infraestructure.Helpers;
using Infraestructure.Models;
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
        private readonly IConfiguration _configuration;
        private readonly IUserRepository Repository;

        public AccountController(IUserRepository repository, IConfiguration iConfiguration)
        {
            this._configuration = iConfiguration;
            Repository = repository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            List<User> result = this.Repository.GetAll().ToList();

            foreach (var item in result)
            {
                item.Password = string.Empty;
            }

            return this.Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetProduct([FromRoute] int id)
        {
            User user;

            try
            {
                user = await this.Repository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Password = string.Empty;
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return user;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Repository.ValidateEmail(model.Email))
                    {
                        return BadRequest("Email already registered.");
                    }

                    model.isActive = true;
                    model.Password = Security.sha256_hash(model.Password);
                    await Repository.CreateAsync(model);
                    return BuildToken(model);
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
            User tmpUser;

            if (ModelState.IsValid)
            {
                User.Password = Security.sha256_hash(User.Password);
                tmpUser = await Repository.ValidateEmailAndPassword(User.Email, User.Password);    

                if (tmpUser != null)
                {
                    if (tmpUser.isActive)
                    {
                        return BuildToken(User);
                    }
                    else
                    {
                        return BadRequest("Blocked user.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
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

                user.Password = Security.sha256_hash(user.Password);
                await this.Repository.UpdateAsync(user);
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
            User user;

            try
            {
                user = await this.Repository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.isActive = false;
                await this.Repository.UpdateAsync(user);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private IActionResult BuildToken(User User)
        {
            Claim[] claims;
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SuperKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token;

            claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, User.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration
            });
        }
    }
}
