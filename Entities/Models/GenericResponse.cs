using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class GenericResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int id { get; set; }
    }
}
