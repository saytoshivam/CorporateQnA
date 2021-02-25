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
    class UserService : IUserService
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
            var userDetails = Mapper.Map<IEnumerable<UserDetails>>(
                Db.GetAll<Data.UserDetails>()).ToList();


            var userLikeCount = userDetails.GroupBy(p => p.Id).ToDictionary(value => value.Key,value => value.Sum(e => e.Likes));
            var userDislikeCount = userDetails.GroupBy(p => p.Id).ToDictionary(value => value.Key, value => value.Sum(e => e.DisLikes));

            return userDetails.GroupBy(user => user.Id).Select(user =>
              {
                  var uniqueUser = user.First();
                  uniqueUser.Likes = userLikeCount.GetValueOrDefault(uniqueUser.Id);
                  uniqueUser.DisLikes = userDislikeCount.GetValueOrDefault(uniqueUser.Id);

                  return uniqueUser;
              }
            );
        }
    }
}
