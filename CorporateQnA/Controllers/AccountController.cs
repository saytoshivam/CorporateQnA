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
    public class AccountController : BaseApiController
    {
        private readonly IAccountService AccountService;
        public AccountController(IAccountService accountService)
        {
            AccountService = accountService;
        }
        [Route("register")]
        public async Task<Object> PostApplicationUser(ApplicationUser model)
        {
            return  await AccountService.PostApplicationUser(model);
        }

       
        [Route("testing")]
        [Authorize]
        public string ForTesting()
        {
            return "Working Fine";
        }

        [Route("login")]
        public async Task<string> Login(Login userCredentials)
        {
            return await  AccountService.Login(userCredentials);
        }
    }
}
