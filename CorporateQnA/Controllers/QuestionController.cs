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
    [Route("api/question/{userId}")]
    public class QuestionController : BaseApiController
    {
        private readonly IQuestionService QuestionService;
        public QuestionController(IQuestionService questionService)
        {
            QuestionService = questionService;
        }

        //[Authorize]
        [HttpPost("~/api/question")]
        public int PostQuestion(Question question)
        {
            return QuestionService.PostQuestion(question);
        }

        //[Authorize]
        [HttpGet("all")]
        public IEnumerable<QuestionDetails> GetQuestionsByUserId(int userId)
        {
            return QuestionService.GetQuestionsByUserId(userId);
        }

        //[Authorize]
        [HttpGet("~/api/question/details")]
        public IEnumerable<QuestionDetails> GetQuestionDetails()
        {
            return QuestionService.GetQuestionDetails();
        }

        //[Authorize]
        [HttpPost("report/{questionId}")]
        public bool ReportQuestion(int questionId,int userId)
        {
            return QuestionService.ReportQuestion(questionId,userId);
        }

        //[Authorize]
        [HttpPost("upvote/{questionId}")]
        public bool UpVoteQuestion(int questionId,int userId)
        {
            return QuestionService.UpVoteQuestion(questionId,userId);
        }
    }
}
