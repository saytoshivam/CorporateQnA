using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    class UserService:IUserService
    {
        private readonly IDbConnection Db;
        private readonly IMapper Mapper;
        public UserService(IDbConnectionService conn, IMapper mapper)
        {
            Mapper = mapper;
            Db = conn.GetDbConnection();
        }

        public IEnumerable<UserDetails> GetUsersDetails()
        {
            return Mapper.Map<IEnumerable<UserDetails>>(
                Db.GetAll<Data.UserDetails>().ToList());

        }
    }
}
