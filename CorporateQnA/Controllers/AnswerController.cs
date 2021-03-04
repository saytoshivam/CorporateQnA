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
    [Route("api/answer/{userId}")]
    public class AnswerController : BaseApiController
    {
        private readonly IAnswerService AnswerService;
       
        public AnswerController(IAnswerService answerService)
        {
            AnswerService = answerService;
        }

        [Authorize]
        [HttpPost("like/{answerId}")]
        public bool LikeAnswer(int answerId,int userId)
        {
           return  AnswerService.LikeAnswer(answerId,userId);
        }

        [Authorize]
        [HttpPost("dislike/{answerId}")]
        public bool DislikeAnswer(int answerId,int userId)
        {
            return AnswerService.DislikeAnswer(answerId,userId);
        }

        [Authorize]
        [HttpGet("details/{questionId}")]
        public IEnumerable<AnswerDetails> GetAnswersDetailsByQuestionId(int questionId,int userId)
        {
            return AnswerService.GetAnswersDetailsByQuestionId(questionId,userId);
        }

        [Authorize]
        [HttpPost("~/api/answer/add")]
        public int PostAnswer(Answer answer)
        {
            return AnswerService.PostAnswer(answer);
        }

        [HttpPost("marksolution/{answerId}")]
        public bool MarkAsBestSolution(int userId,int answerId)
        {
            return AnswerService.MarkAsBestSolution(userId,answerId);
        }
    }
}