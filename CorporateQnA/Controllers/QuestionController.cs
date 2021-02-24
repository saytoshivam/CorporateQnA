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
    public class QuestionController : BaseApiController
    {
        private readonly IQuestionService QuestionService;
        public QuestionController(IQuestionService questionService)
        {
            QuestionService = questionService;
        }

        [Authorize]
        [HttpPost]
        public void PostQuestion(Question question)
        {
            QuestionService.PostQuestion(question);
        }

        [Authorize]
        [HttpGet("userid/{id}")]
        public IEnumerable<QuestionDetails> GetQuestionsByUserId(int id)
        {
            return QuestionService.GetQuestionsByUserId(id);
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<QuestionDetails> GetQuestionDetails()
        {
            return QuestionService.GetQuestionDetails();
        }

        [Authorize]
        [HttpPost("report/{questionId}/{userId}")]
        public bool ReportQuestion(int questionId,int userId)
        {
            return QuestionService.ReportQuestion(questionId,userId);
        }

        [Authorize]
        [HttpPost("upvote/{questionId}/{userId}")]
        public bool UpVoteQuestion(int questionId,int userId)
        {
            return QuestionService.UpVoteQuestion(questionId,userId);
        }
    }
}
