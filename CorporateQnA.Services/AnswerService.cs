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

        public void MarkAsBestSolution(int userId,int answerId)
        {
            string query = $"Select questionId from Answers where id={answerId}";
            int questionId=Db.QueryFirstOrDefault<int>(query);

            if(questionId!=0)
            {
                query = $"Select askedBy from Questions where id={questionId}";
                int askedBy = Db.QueryFirstOrDefault<int>(query);
                if(askedBy==userId)
                {
                    bool isBestSolution=Db.QueryFirstOrDefault<bool>($"Select [IsBestSolution] from Answers where id={answerId}");
                    query = $"Update [Answers] SET IsBestSolution = '{!isBestSolution}' where id= {answerId}";
                    Db.Execute(query);
                }

            }
        }

        public void PostAnswer(Answer answer)
        {
            answer.AnsweredOn = DateTime.Now;
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
                query = $"UPDATE [Questions]  SET ViewedBy = '{views}' WHERE Id = {questionId}";
                Db.Execute(query);
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
            string query = $"UPDATE [Answers]  SET dislikedBy = '{dislikes}'  WHERE Id = {answerId}";
            Db.Execute(query);
        }
        private void UpdateLikes(List<int> likedBy, int answerId)
        {
            string likes = JsonConvert.SerializeObject(likedBy);
            string query = $"UPDATE [Answers]  SET LikedBy = '{likes}'  WHERE Id = {answerId}";
            Db.Execute(query);
        }
    }
}