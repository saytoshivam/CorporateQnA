using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Models.Enum;
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
    class QuestionService : IQuestionService
    {
        private readonly IDbConnection Db;

        private readonly IMapper Mapper;

        public QuestionService(IDbConnectionService conn, IMapper mapper)
        {
            Db = conn.GetDbConnection();
            Mapper = mapper;
        }

        public int PostQuestion(Question question)
        {
            question.AskedOn = DateTime.Now;
            return (int)Db.Insert(Mapper.Map<Data.Question>(question));
        }
       
        public IEnumerable<QuestionDetails> GetQuestionDetails()
        {
            return Mapper.Map<IEnumerable<QuestionDetails>>(Db.GetAll<Data.QuestionDetails>());   
        }

        public IEnumerable<QuestionDetails> GetQuestionsByUserId(int id)
        {
            return Mapper.Map<IEnumerable<QuestionDetails>>(Db.Query<Data.QuestionDetails>("GetQuestionsDetailsByUserId"
                , new {UserId=id }, commandType:CommandType.StoredProcedure).ToList());
        }

        public bool ReportQuestion(int questionId,int userId)
        {
            string query = $"Select ReportedBy from Questions Where id={questionId}";
            string reports = Db.QueryFirstOrDefault<string>(query);

            var reportedBy= JsonConvert.DeserializeObject<List<int>>(reports ?? "[]");
          
            if (reportedBy.Contains(userId))
                return false;

            reportedBy.Add(userId);
            reports=JsonConvert.SerializeObject(reportedBy);

            query = $"UPDATE [Questions]  SET ReportedBy = '{reports}' WHERE Id = {questionId}";
            Db.Execute(query);

            return true;
        }

        public bool UpVoteQuestion(int questionId, int userId)
        {
            
            string query = $"Select VotedBy from Questions Where id={questionId}";
            string upVotes=  Db.QueryFirstOrDefault<string>(query);
          
            List<int> upVotedBy = JsonConvert.DeserializeObject<List<int>>(upVotes??"[]");

            if (upVotedBy.Contains(userId))
                return false;

            upVotedBy.Add(userId);

            upVotes=JsonConvert.SerializeObject(upVotedBy);

             query = $"UPDATE [Questions] SET VotedBy = '{upVotes}' WHERE Id = {questionId}";
             Db.Execute(query);
              return true;
        }

        public IEnumerable<QuestionDetails> GetFilteredQuestions(List<Filter> filters)
        {

            var sql = "SELECT * FROM QuestionsView";
            bool isWhereUsed = false;

            filters.ForEach(filter =>
            {
                sql = $"{sql} {(isWhereUsed ? "AND" : "WHERE")} {filter.ColumnName} {(filter.Type == filterType.Date ? $"BETWEEN '{DateTime.Now.AddDays(-Convert.ToInt64(filter.Id))}' AND '{DateTime.Now}'" : (filter.Type == filterType.Boolean ? (filter.IsNullCheck ? "IS NULL" : " IS NOT NULL") : (filter.Type == filterType.Integer ? $"= {filter.Id}" : $"={ filter.Id}")))}";
                isWhereUsed = true;
            });

            return this.Mapper.Map<List<QuestionDetails>>(this.Db.Query<Data.QuestionDetails>(sql).ToList());
        }
    }
}
