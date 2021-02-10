using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Interfaces
{
    public interface IAccountService
    {
        public  Task<Object> PostApplicationUser(ApplicationUser model);
        public  Task<string> Login(Login login);
    }
}
