using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using CorporateQnA.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CorporateQnA.Models;

namespace CorporateQnA.Services
{
    class AccountService:IAccountService
    {
        private readonly ITokenService TokenRepository;
        private readonly UserManager<Data.ApplicationUser> UserManager;
        private readonly IMapper Mapper;
        public AccountService(ITokenService tokenRepository,IMapper mapper, UserManager<Data.ApplicationUser> userManager)
        {
            UserManager = userManager;
            Mapper = mapper;
            TokenRepository = tokenRepository;
        }

        public async Task<Object> PostApplicationUser(ApplicationUser model)
        { 
            return await UserManager.CreateAsync(Mapper.Map<Data.ApplicationUser>(model),model.Password);
        }

        public async Task<string> Login(Login login)
        {
            var user = await UserManager.FindByNameAsync(login.UserName);
            if (user != null && await UserManager.CheckPasswordAsync(user, login.Password))
               return  TokenRepository.CreateToken(user);
            return null;
        }
    }
}