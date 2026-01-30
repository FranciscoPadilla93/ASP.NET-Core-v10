using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class UserRefreshToken
    {
        public int idRefreshToken { get; set; }
        public int idUser { get; set; }
        public string token { get; set; }
        public string jwtId { get; set; }
        public bool isUsed { get; set; }
        public bool isRevoked { get; set; }
        public DateTime addedDate { get; set; }
        public DateTime expiryDate { get; set; }
    }
}
