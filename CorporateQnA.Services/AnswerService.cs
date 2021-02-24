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

        public IEnumerable<AnswerDetails> GetAnswersDetailsByQuestionId(int questionId,int userId)
        {
            AddView(questionId,userId);
            return Mapper.Map<IEnumerable<AnswerDetails>>(Db.Query<Data.StoredProcedureModels.AnswerDetails>("GetAnswersDetailsByQuestionId"
               , new { QuestionId = questionId }, commandType: CommandType.StoredProcedure).ToList());
        }

        private void AddView(int questionId,int userId)
        {
            string query = $"Select ViewedBy from Questions Where id={questionId}";

            string views = Db.QueryFirstOrDefault<string>(query);
            List<int> viewedBy = new List<int>();
            if(views!=null)
            {
                viewedBy = JsonConvert.DeserializeObject<List<int>>(views);
            }

            if (!viewedBy.Contains(userId))
            { 
                viewedBy.Add(userId);
                views = JsonConvert.SerializeObject(viewedBy);
                Db.Execute(@"UPDATE [Questions] 
                              SET ViewedBy = @ViewedBy
                             WHERE Id = @QuestionId",
                            new { ViewedBy = views, QuestionId = questionId }
                );
            }
        }

        public void LikeAnswer(int answerId,int userId)
        {
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

        public void DislikeAnswer(int answerId,int userId)
        {
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
            string query = $"Select LikedBy from Answers Where id={answerId}";
            var likes = Db.QueryFirstOrDefault<string>(query);
            if (likes == null)
                return new List<int>();

            return JsonConvert.DeserializeObject<List<int>>(likes);
        }

        private List<int> GetDislikesList(int answerId)
        {
            string query = $"Select dislikedBy from Answers Where id={answerId}";

            var dislikes = Db.QueryFirstOrDefault<string>(query);

            if (dislikes == null)
                return new List<int>();
            return (JsonConvert.DeserializeObject<List<int>>(dislikes));
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