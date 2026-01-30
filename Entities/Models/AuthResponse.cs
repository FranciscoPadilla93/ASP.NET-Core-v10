using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
