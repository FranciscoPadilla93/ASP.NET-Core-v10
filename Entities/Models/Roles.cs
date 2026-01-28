using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Entities.Models
{
    public class Roles
    {
        public int idRol { get; set; }
        public string rolName { get; set; } = string.Empty;
        public string rolDescription { get; set; } = string.Empty;
        public DateTime dateAdd { get; set; }
        public string userAdd { get; set; } = string.Empty;
        public DateTime dateMod { get; set; }
        public string userMod { get; set; } = string.Empty;
    }

    public class RolesSet
    {
        public int idRol { get; set; }
        public string rolName { get; set; } = string.Empty;
        public string rolDescription { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string userAction { get; set; } = string.Empty;
    }
}
