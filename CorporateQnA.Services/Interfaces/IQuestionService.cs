using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Interfaces
{
    public interface IQuestionService
    {
        public void PostQuestion(Question question);
        public IEnumerable<QuestionDetails> GetQuestionDetails();
        public IEnumerable<QuestionDetails> GetQuestionsByUserId(int id);
        public bool ReportQuestion(int questionId);
        public bool UpVoteQuestion(int questionId);
    }
}
