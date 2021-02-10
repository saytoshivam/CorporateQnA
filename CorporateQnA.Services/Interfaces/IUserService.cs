using System;
using System.Collections.Generic;
using CorporateQnA.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserDetails> GetUsersDetails();
    }
}
