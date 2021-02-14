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
            var temp = Db.GetAll<Data.UserDetails>().ToList().GroupBy(x => new
            {
                x.Department,
                x.UserImage,
                x.Designation,
                x.FullName,
                x.Id,
                x.JobLocation,
                x.QuestionsAnswered,
                x.QuestionsAsked,
                x.QuestionsSolved
            }).Select(b => new UserDetails
            {
                Id = b.Key.Id,
                FullName = b.Key.FullName,
                Likes = LikesCount(b.Select(x => x.LikedBy).ToList())

            });
            return temp;//Mapper.Map<UserDetails>(temp);
        }
        private static int  LikesCount(List<string> likes)
        {
            int likeCount = 0;
            foreach (string like in likes)
                likeCount += JsonConvert.DeserializeObject<List<int>>(like).Count;
            return likeCount;
        }
    }
}
