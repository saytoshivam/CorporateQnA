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
        private readonly ITokenService TokenService;
        public QuestionService(IDbConnectionService conn, IMapper mapper,ITokenService tokenServie)
        {
            Db = conn.GetDbConnection();
            Mapper = mapper;
            TokenService = tokenServie;
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

        public bool ReportQuestion(int questionId)
        {
            int userId = 12;//need to be fetched from token
            string query = "Select ReportedBy from Questions Where id=@Id";

            List<int> reportedBy=JsonConvert.DeserializeObject<List<int>>(Db.Query<string>(query,
                new { Id = questionId }).Single());

            if (reportedBy.Contains(userId))
                return false;

            reportedBy.Add(userId);
            string reports=JsonConvert.SerializeObject(reportedBy);

            query = "UPDATE [Questions]  SET ReportedBy = @ReportedBy WHERE Id = @QuestionId";
            Db.Execute(query,new { QuestionId=questionId, ReportedBy=reports });

            return true;
        }

        public bool UpVoteQuestion(int questionId)
        {
            int userId = 12;
            string query = "Select VotedBy from Questions Where id=@Id";

            List<int> upVotedBy = JsonConvert.DeserializeObject<List<int>>(Db.Query<string>(query,
                new { Id = questionId }).Single());

            if (upVotedBy.Contains(userId))
                return false;
            upVotedBy.Add(userId);

            string upVotes=JsonConvert.SerializeObject(upVotedBy);
            Db.Execute(@"UPDATE [Questions] 
                                 SET VotedBy = @UpVotedBy
                                 WHERE Id = @QuestionId",
                                new { UpVotedBy = upVotes, QuestionId = questionId }
                                );
              return true;
        }
    }
}
