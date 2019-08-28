using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Dto
{
    public class SecurityLoginDto
    {
        public int Rol { get; set; }
        public bool IsActive{ get; set; }
        public DateTime Expiration { get; set; }
        public string WebToken { get; set; }
    }
}
