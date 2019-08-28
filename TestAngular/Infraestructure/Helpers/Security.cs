using Infraestructure.Dto;
using Infraestructure.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infraestructure.Helpers
{
	public class Security
	{
		protected Security()
		{
		}

		public static string Sha256_hash(string value)
		{
			string finalKey;
			byte[] encode;
			encode = Encoding.UTF8.GetBytes(value);
			finalKey = Convert.ToBase64String(encode);
			return finalKey;
		}

		public static bool CompareHash(string firstText, string secondText)
		{
			bool result = false;
			string firstValue = Sha256_hash(firstText);
			string secondValue = Sha256_hash(secondText);

			if (firstValue == secondValue)
			{
				result = true;
			}

			return result;
		}

		public static jwtDto BuildToken(User User, string Secretkey)
		{
			Claim[] claims;
			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));
			SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			DateTime expiration = DateTime.UtcNow.AddHours(1);
			JwtSecurityToken token;
			jwtDto jwtDto;

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

			jwtDto = new jwtDto
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiration = expiration
			};

			return jwtDto;
		}
	}
}
