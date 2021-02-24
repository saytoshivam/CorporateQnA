using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserService UserService;
        public UserController(IUserService userService)
        {
            UserService = userService;
        }


        [Authorize]
        [HttpGet]
        public IEnumerable<UserDetails> GetUsersDetails()
        {
            return UserService.GetUsersDetails();
        }
    }
}
