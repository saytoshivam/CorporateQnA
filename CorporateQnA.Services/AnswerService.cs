using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    class AnswerService : IAnswerService
    {
        private readonly IDbConnection Db;
        private readonly IMapper Mapper;
        public AnswerService(IDbConnectionService conn,IMapper mapper)
        {
            Mapper = mapper;
            Db = conn.GetDbConnection();
        }

        public void PostAnswer(Answer answer)
        {
            Db.Insert(Mapper.Map<Data.Answer>(answer));
        }

        public IEnumerable<AnswerDetails> GetAnswersDetailsByQuestionId(int questionId)
        {
            AddView(questionId);
            return Mapper.Map<IEnumerable<AnswerDetails>>(Db.Query<Data.StoredProcedureModels.AnswerDetails>("GetAnswersDetailsByQuestionId"
               , new { QuestionId = questionId }, commandType: CommandType.StoredProcedure).ToList());
        }

        private void AddView(int questionId)
        {
            int userId = 12;//need to be fetch from token
            string query = "Select ViewedBy from Questions Where id=@Id";

            List<int> viewedBy= JsonConvert.DeserializeObject<List<int>>(Db.Query<string>(query, 
                new { Id = questionId }).Single());


            if (!viewedBy.Contains(userId))
            { 
                viewedBy.Add(userId);
                string views = JsonConvert.SerializeObject(viewedBy);
                Db.Execute(@"UPDATE [Questions] 
                              SET ViewedBy = @ViewedBy
                             WHERE Id = @QuestionId",
                            new { ViewedBy = views, QuestionId = questionId }
                );
            }
        }

        public void LikeAnswer(int answerId)
        {
            int userId = 3;
            List<int> likedBy = GetLikesList(answerId);

            if (!likedBy.Contains(userId))
            {
                likedBy.Add(userId);
                List<int> dislikeList = GetDislikesList(answerId);
                if (dislikeList.Contains(userId))
                {
                    dislikeList.Remove(userId);
                    UpdateDislikes(dislikeList, answerId);
                }
            }
            else
            {
                likedBy.Remove(userId);
            }

            UpdateLikes(likedBy,answerId);        
        }

        public void DislikeAnswer(int answerId)
        {
            int userId = 3;
            List<int> dislikedBy = GetDislikesList(answerId);
            if (!dislikedBy.Contains(userId))
            {
                dislikedBy.Add(userId);
                List<int> likeList = GetLikesList(answerId);
                if (likeList.Contains(userId))
                {
                    likeList.Remove(userId);
                    UpdateLikes(likeList, answerId);
                }
            }
            else
                dislikedBy.Remove(userId);

            UpdateDislikes(dislikedBy,answerId);
        }
        private List<int> GetLikesList(int answerId)
        {
            string query = "Select LikedBy from Answers Where id=@Id";

            return JsonConvert.DeserializeObject<List<int>>(Db.Query<string>(query,
                 new { Id = answerId }).Single());
        }
        private List<int> GetDislikesList(int answerId)
        {
            string query = "Select dislikedBy from Answers Where id=@Id";
            return (JsonConvert.DeserializeObject<List<int>>(Db.Query<string>(query,
                new { Id = answerId }).Single()));
        }
        private void UpdateDislikes(List<int> dislikedBy,int answerId)
        {
            string dislikes = JsonConvert.SerializeObject(dislikedBy);
            Db.Execute(@"UPDATE [Answers] 
                                 SET dislikedBy = @dislikedBy
                                 WHERE Id = @AnswerId",
                             new { dislikedBy = dislikes, AnswerId = answerId }
                             );
        }
        private void UpdateLikes(List<int> likedBy, int answerId)
        {
            string likes = JsonConvert.SerializeObject(likedBy);
            Db.Execute(@"UPDATE [Answers] 
                                 SET LikedBy = @LikedBy
                                 WHERE Id = @AnswerId",
                             new { LikedBy = likes, AnswerId = answerId }
                             );
        }
    }
}