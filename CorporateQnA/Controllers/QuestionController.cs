using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
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
        public void PostQuestion(Question question)
        {
            QuestionService.PostQuestion(question);
        }
        [Route("{id}")]
        public IEnumerable<QuestionDetails> GetQuestionsByUserId(int id)
        {
            return QuestionService.GetQuestionsByUserId(id);
        }
        public IEnumerable<QuestionDetails> GetQuestionDetails()
        {
            return QuestionService.GetQuestionDetails();
        }
        [Route("report/{questionId}/{userId}")]
        public bool ReportQuestion(int questionId,int userId)
        {
            return QuestionService.ReportQuestion(questionId,userId);
        }
        [Route("upvote/{questionId}/{userId}")]
        public bool UpVoteQuestion(int questionId,int userId)
        {
            return QuestionService.UpVoteQuestion(questionId,userId);
        }
    }
}
