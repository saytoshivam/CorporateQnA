using AutoMapper;
using CorporateQnA.Models.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using CorporateQnA.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                var registerUser = Mapper.Map<Data.ApplicationUser>(model);
                try
                {
                    var result = await UserManager.CreateAsync(registerUser, model.Password);
                    return result;
                }
                catch (Exception)
                {
                    return "Issue occurs";
                }
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