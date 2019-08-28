using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Dto
{
	public class jwtDto
	{
		public string Token { get; set; }
		public DateTime Expiration { get; set; }
	}
}
