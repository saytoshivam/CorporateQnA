using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Interfaces
{
    public interface IAnswerService
    {
        public IEnumerable<AnswerDetails> GetAnswersDetailsByQuestionId(int questionId);
        public void PostAnswer(Answer answer);
        public void LikeAnswer(int answerId);
        public void DislikeAnswer(int answerId);
    }
}
