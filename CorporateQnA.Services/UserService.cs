using AutoMapper;
using CorporateQnA.Models.Core;
using CorporateQnA.Services.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
        public UserService(IConfiguration config, IMapper mapper)
        {
            Mapper = mapper;
            Db = new SqlConnection(config.GetConnectionString("CorporateQnAContext"));
            Db.Open();
        }

        public IEnumerable<UserDetails> GetUsersDetails()
        {
            IEnumerable<UserDetails> usersDetails = Mapper.Map<IEnumerable<UserDetails>>(
                Db.GetAll<Data.UserDetails>());

            foreach(UserDetails userDetails in usersDetails)
            {
                LikeDislikeCount likeDislikeCount = this.GetTotalLikesDislikes(userDetails.Id);
                userDetails.Likes = likeDislikeCount.LikesCount;
                userDetails.DisLikes = likeDislikeCount.DislikesCount;
            }
            return usersDetails;
        }
        private LikeDislikeCount GetTotalLikesDislikes(int id)
        {
            string query = "Select LikedBy,DisLikedBy from Answers Where AnsweredBy=@Id";
            List<LikeDislike> likeDislikes = Db.Query<LikeDislike>(query,new {@Id=id }).ToList();
            string likes = "";
            string dislikes = "";
            foreach (LikeDislike t in likeDislikes)
            {
                if (t != null)
                {
                    likes+= t.LikedBy + ",";
                    dislikes += t.DisLikedBy + ",";
                }
            }
            string[] likesList = likes.Split(",");
            string[] disLikesList = dislikes.Split(",");
            LikeDislikeCount likeDislikeCount=new LikeDislikeCount();
            likeDislikeCount.LikesCount = likesList.Where(likes => likes != "").Count();
            likeDislikeCount.DislikesCount = disLikesList.Where(disLikes => disLikes != "").Count();

            return likeDislikeCount;
        }
    }
}
