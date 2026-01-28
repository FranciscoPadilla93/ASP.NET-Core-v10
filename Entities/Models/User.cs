using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Entities.Models
{
    public class User
    {
        public int idUser { get; set; }
        public int idRol { get; set; }
        public string loginUser { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime dateAdd { get; set; }
        public string userAdd { get; set; } = string.Empty;
        public DateTime dateMod { get; set; }
        public string userMod { get; set; } = string.Empty;
        public DateTime dateDel { get; set; }
        public string userDel { get; set; } = string.Empty;
    }

    public class UserSet
    {
        public int idUser { get; set; }
        public int idRol { get; set; }
        public string loginUser { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string userAction { get; set; } = string.Empty;
    }
}
