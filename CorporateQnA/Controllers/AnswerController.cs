using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateQnA.Controllers
{
    public class AnswerController : BaseApiController
    {
        private readonly IAnswerService AnswerRepositiory;
       
        public AnswerController(IAnswerService answerRepositiory)
        {
            AnswerRepositiory = answerRepositiory;
        }
        [Route("like/{answerId}")]
        public void LikeAnswer(int answerId)
        {
            AnswerRepositiory.LikeAnswer(answerId);
        }
        [Route("dislike/{answerId}")]
        public void DislikeAnswer(int answerId)
        {
            AnswerRepositiory.DislikeAnswer(answerId);
        }
        [Route("{questionId}")]
        public IEnumerable<AnswerDetails> GetAnswersDetailsByQuestionId(int questionId)
        {
            return AnswerRepositiory.GetAnswersDetailsByQuestionId(questionId);
        }

        [Authorize]
        [HttpPost]
        public void PostAnswer(Answer answer)
        {
            AnswerRepositiory.PostAnswer(answer);
        }
    }
}
