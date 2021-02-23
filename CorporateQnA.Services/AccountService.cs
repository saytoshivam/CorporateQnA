using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using CorporateQnA.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CorporateQnA.Models;
using Microsoft.AspNetCore.Http;

namespace CorporateQnA.Services
{
    class AccountService:IAccountService
    {
        private readonly ITokenService TokenService;
        private readonly UserManager<Data.ApplicationUser> UserManager;
        private readonly IMapper Mapper;
        public AccountService(ITokenService tokenService,IMapper mapper, UserManager<Data.ApplicationUser> userManager)
        {
            UserManager = userManager;
            Mapper = mapper;
            TokenService = tokenService;
        }

        public async Task<Object> PostApplicationUser(ApplicationUser model)
        { 
            return await UserManager.CreateAsync(Mapper.Map<Data.ApplicationUser>(model),model.Password);
        }

        public async Task<string> Login(Login login)
        {
            var user = await UserManager.FindByNameAsync(login.UserName);
            if (user != null && await UserManager.CheckPasswordAsync(user, login.Password))
            {
                TokenService.CreateToken(user);
            }
            return null;
        }
    }
}