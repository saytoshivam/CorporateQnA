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
        private readonly IAnswerService AnswerService;
       
        public AnswerController(IAnswerService answerService)
        {
            AnswerService = answerService;
        }
        [Route("like/{answerId}/{userId}")]
        public void LikeAnswer(int answerId,int userId)
        {
            AnswerService.LikeAnswer(answerId,userId);
        }
        [Route("dislike/{answerId}/{userId}")]
        public void DislikeAnswer(int answerId,int userId)
        {
            AnswerService.DislikeAnswer(answerId,userId);
        }
        [Route("{questionId}")]
        public IEnumerable<AnswerDetails> GetAnswersDetailsByQuestionId(int questionId)
        {
            return AnswerService.GetAnswersDetailsByQuestionId(questionId);
        }

        [Authorize]
        public void PostAnswer(Answer answer)
        {
            AnswerService.PostAnswer(answer);
        }
    }
}
