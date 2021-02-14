using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
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
            Db.Insert(answer);
        }

        public IEnumerable<AnswerDetails> GetAnswersDetailsByQuestionId(int id)
        {
            return Mapper.Map<IEnumerable<AnswerDetails>>(Db.Query<Data.StoredProcedureModels.AnswerDetails>("GetAnswersDetailsByQuestionId"
               , new { QuestionId = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        private void AddView(int userId,int questionId)
        {
            string query = "Select ViewedBy from Questions Where id=@Id";

            List<int> viewedBy = StringToListConverter(Db.Query<string>(query,
                new { Id = questionId }).Single()).ToList();

            if (!viewedBy.Contains(userId))
            {
                viewedBy.Add(userId);
                string viewedByString = ListToStringConverter(viewedBy);
                Db.Execute(@"UPDATE [Questions] 
                                 SET ViewedBy = @ViewedBy
                                 WHERE Id = @QuestionId",
                                 new { ViewedBy = viewedByString, QuestionId = questionId }
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
                likedBy.Remove(userId);

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
            return StringToListConverter(Db.Query<string>(query,
                 new { Id = answerId }).Single()).ToList();
        }
        private List<int> GetDislikesList(int answerId)
        {
            string query = "Select dislikedBy from Answers Where id=@Id";
            return StringToListConverter(Db.Query<string>(query,
                new { Id = answerId }).Single()).ToList();
        }
        private void UpdateDislikes(List<int> dislikedBy,int answerId)
        {
            string dislikedByString = ListToStringConverter(dislikedBy);
            Db.Execute(@"UPDATE [Answers] 
                                 SET dislikedBy = @dislikedBy
                                 WHERE Id = @AnswerId",
                             new { dislikedBy = dislikedByString, AnswerId = answerId }
                             );
        }
        private void UpdateLikes(List<int> likedBy, int answerId)
        {
            string likedByString = ListToStringConverter(likedBy);
            Db.Execute(@"UPDATE [Answers] 
                                 SET LikedBy = @LikedBy
                                 WHERE Id = @AnswerId",
                             new { LikedBy = likedByString, AnswerId = answerId }
                             );
        }
        private IEnumerable<int> StringToListConverter(string s)
        {
            if (s == null)
                return new List<int>();
            return s.Split(',').Select(int.Parse);
        }
        private string ListToStringConverter(List<int> list)
        {
            if (list.Count() == 0)
                return null;
            return string.Join(",", list.Select(x => x.ToString()));
        }
    }
}
