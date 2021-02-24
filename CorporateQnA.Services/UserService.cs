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
            IEnumerable<UserDetails> userData = Mapper.Map<IEnumerable<UserDetails>>(
                Db.GetAll<Data.UserDetails>().ToList());
           

           var userLikeCount =  userData.GroupBy(p => p.Id, p => p.Likes, (key, g) => new { id = key, likes = g.ToList().Sum(z=>z)});
           var userDislikeCount = userData.GroupBy(p => p.Id, p => p.DisLikes, (key, g) => new { id = key, dislikes = g.ToList().Sum(z => z) });

            var joined = from i in userLikeCount join j in userDislikeCount on i.id equals j.id select new { Id = i.id, likes = i.likes, dislikes = j.dislikes };

            var userDetails = from i in joined join j in userData on i.Id equals j.Id select new UserDetails
            { 
                Id = i.Id, 
                FullName = j.FullName, 
                JobLocation = j.JobLocation,
                Department = j.Department,
                Designation = j.Designation,
                UserImage = j.UserImage,
                Likes = i.likes,
                DisLikes = i.dislikes,
                QuestionsAsked = j.QuestionsAsked,
                QuestionsSolved = j.QuestionsSolved, 
                QuestionsAnswered = j.QuestionsAnswered 
            };

            return userDetails.GroupBy(e=>e.Id).Select(e=>e.First());
        }
    }
}
