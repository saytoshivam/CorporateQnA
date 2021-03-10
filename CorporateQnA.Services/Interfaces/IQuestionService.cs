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
        public int PostQuestion(Question question);

        public IEnumerable<QuestionDetails> GetQuestionDetails();

        public IEnumerable<QuestionDetails> GetQuestionsByUserId(int id);

        public bool ReportQuestion(int questionId,int userId);

        public bool UpVoteQuestion(int questionId,int userId);

        public IEnumerable<QuestionDetails> GetFilteredQuestions(List<Filter> filter);
    }
}
