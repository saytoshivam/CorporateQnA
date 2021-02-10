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
    class QuestionService : IQuestionService
    {
        private readonly IDbConnection Db;
        private readonly IMapper Mapper;
        public QuestionService(IDbConnectionService conn, IMapper mapper)
        {
            Db = conn.GetDbConnection();
            Mapper = mapper;
        }

        public void PostQuestion(Question question)
        {
            Db.Insert(Mapper.Map<Data.Question>(question));
        }
       
        public IEnumerable<QuestionDetails> GetQuestionDetails()
        {
            return Mapper.Map<IEnumerable<QuestionDetails>>(Db.GetAll<Data.QuestionDetails>().ToList());   
        }

        public IEnumerable<QuestionDetails> GetQuestionsByUserId(int id)
        {
            return Mapper.Map<IEnumerable<QuestionDetails>>(Db.Query<Data.QuestionDetails>("GetQuestionsDetailsByUserId"
                , new {UserId=id }, commandType:CommandType.StoredProcedure).ToList());
        }

        public bool ReportQuestion(int questionId)
        {
            int UserId = 6;
            string query = "Select ReportedBy from Questions Where id=@Id";

            List<int> reportedBy = StringToListConverter(Db.Query<string>(query,
                new { Id = questionId }).Single()).ToList();

            if (!reportedBy.Contains(UserId))
            {
                reportedBy.Add(UserId);
                string reportedByString = ListToStringConverter(reportedBy);
                Db.Execute(@"UPDATE [Questions] 
                                 SET ReportedBy = @ReportedBy
                                 WHERE Id = @QuestionId",
                                 new { ReportedBy=reportedByString,QuestionId=questionId}
                                 );

                return true;
            }
            return false;

        }

        public bool UpVoteQuestion(int questionId)
        {
            int UserId = 5;
            string query = "Select VotedBy from Questions Where id=@Id";

            List<int> upVotedBy = StringToListConverter(Db.Query<string>(query,
                new { Id = questionId }).Single()).ToList();

            if (!upVotedBy.Contains(UserId))
            {
                upVotedBy.Add(UserId);
                string upVotedByString = ListToStringConverter(upVotedBy);
                Db.Execute(@"UPDATE [Questions] 
                                 SET VotedBy = @UpVotedBy
                                 WHERE Id = @QuestionId",
                                 new { UpVotedBy = upVotedByString, QuestionId = questionId }
                                 );

                return true;
            }
            return false;
        }

        private IEnumerable<int> StringToListConverter(string s)
        {
            if (s == null)
                return new List<int>();
           return  s.Split(',').Select(int.Parse);
           
        }
        private string ListToStringConverter(List<int> list)
        {
            return string.Join(",", list.Select(x => x.ToString()));
        }
    }
}
