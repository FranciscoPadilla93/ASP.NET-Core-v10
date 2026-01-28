using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public interface ILoginService
    {
        Task<string?> Login(string login, string password);
    }
}
