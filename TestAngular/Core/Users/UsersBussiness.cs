using Infraestructure.Dto;
using Infraestructure.GenericRepository;
using Infraestructure.Helpers;
using Infraestructure.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Users
{
	public class UsersBussiness
	{
		private readonly IUserRepository Repository;
		private readonly ILog LogRepository;

		public UsersBussiness(IUserRepository repository, ILog logRepository)
		{
			Repository = repository;
			this.LogRepository = logRepository;
		}

		public List<User> GetAllUsers()
		{
			List<User> result = this.Repository.GetAll().ToList();

			foreach (var item in result)
			{
				item.Password = string.Empty;
			}

			return result;
		}

		public async Task<User> GetUserById(int id)
		{
			User user = await this.Repository.GetByIdAsync(id);
			user.Password = string.Empty;
			return user;
		}

		public async Task<SecurityLoginDto> CreateUser(User model, string secretKey)
		{
			if (Repository.ValidateEmail(model.Email))
			{
				return null;
			}

			model.isActive = true;
			model.Password = Security.Sha256_hash(model.Password);
			await Repository.CreateAsync(model);
			return Security.BuildToken(model, secretKey);
		}

		public async Task<SecurityLoginDto> Login(User user, string superKey)
		{
			User tmpUser;
			user.Password = Security.Sha256_hash(user.Password);
			tmpUser = await Repository.ValidateEmailAndPassword(user.Email, user.Password);
			SecurityLoginDto jwdto;

			if (tmpUser != null)
			{
				if (tmpUser.isActive)
				{
					jwdto = Security.BuildToken(tmpUser, superKey);
					await LogRepository.CreateLogAsync(tmpUser, "Inicio de sesión");
				}
				else
				{
					throw new Exception("Blocked user.");
				}
			}
			else
			{
				throw new Exception("User o password not valid.");
			}

			return jwdto;
		}

		public async Task UpdateUser(User user)
		{
			User tmpUser = await this.Repository.GetByIdAsync(user.Id);
			
			if (string.IsNullOrEmpty(user.Password))
			{
				user.Password = tmpUser.Password;
			}
			else
			{
				user.Password = Security.Sha256_hash(user.Password);
			}

			await this.Repository.UpdateAsync(user);			
		}

		public async Task DisableUser(int id)
		{
			User user = await this.Repository.GetByIdAsync(id);

			if (user == null)
			{
				throw new KeyNotFoundException("User doesn't exist!");
			}

			user.isActive = false;
			await this.Repository.UpdateAsync(user);
		}
	}
}
