using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CorporateQnA.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService UserRepository;
        public UserController(IUserService userReository)
        {
            UserRepository = userReository;
        }

        public IEnumerable<UserDetails> GetUsersDetails()
        {
            return UserRepository.GetUsersDetails();
        }
    }
}
