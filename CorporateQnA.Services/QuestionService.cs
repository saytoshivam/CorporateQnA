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
            question.AskedOn = DateTime.Now;
            Db.Insert(Mapper.Map<Data.Question>(question));
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
            string query = "Select ReportedBy from Questions Where id=@Id";
            string reports = Db.QueryFirstOrDefault<string>(query,new { Id = questionId });

            List<int> reportedBy = new List<int>();

            if(reports!=null)
            {
                reportedBy= JsonConvert.DeserializeObject<List<int>>(reports);
            }

            if (reportedBy.Contains(userId))
                return false;

            reportedBy.Add(userId);
            reports=JsonConvert.SerializeObject(reportedBy);

            query = "UPDATE [Questions]  SET ReportedBy = @ReportedBy WHERE Id = @QuestionId";
            Db.Execute(query,new { QuestionId=questionId, ReportedBy=reports });

            return true;
        }

        public bool UpVoteQuestion(int questionId, int userId)
        {
            
            string query = "Select VotedBy from Questions Where id=@Id";
            string upVotes=  Db.QueryFirstOrDefault<string>(query,new { Id = questionId });
            List<int> upVotedBy = new List<int>();
            if (upVotes != null)
            {
                upVotedBy = JsonConvert.DeserializeObject<List<int>>(upVotes);
            }

            if (upVotedBy.Contains(userId))
                return false;
            upVotedBy.Add(userId);

            upVotes=JsonConvert.SerializeObject(upVotedBy);
            Db.Execute(@"UPDATE [Questions] 
                                 SET VotedBy = @UpVotedBy
                                 WHERE Id = @QuestionId",
                                new { UpVotedBy = upVotes, QuestionId = questionId }
                                );
              return true;
        }
    }
}
