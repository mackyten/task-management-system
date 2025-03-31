using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.DOMAIN.DTOs
{
    public class AuthenticatedUserDTO
    {
        public string TokenType { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
    }
}