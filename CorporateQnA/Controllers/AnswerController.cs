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

        [Authorize]
        [HttpPost("like/{answerId}/{userId}")]
        public void LikeAnswer(int answerId,int userId)
        {
            AnswerService.LikeAnswer(answerId,userId);
        }

        [Authorize]
        [HttpPost("dislike/{answerId}/{userId}")]
        public void DislikeAnswer(int answerId,int userId)
        {
            AnswerService.DislikeAnswer(answerId,userId);
        }

        [Authorize]
        [HttpGet("{questionId}/{userId}")]
        public IEnumerable<AnswerDetails> GetAnswersDetailsByQuestionId(int questionId,int userId)
        {
            return AnswerService.GetAnswersDetailsByQuestionId(questionId,userId);
        }

        [Authorize]
        [HttpPost]
        public void PostAnswer(Answer answer)
        {
            AnswerService.PostAnswer(answer);
        }
    }
}
