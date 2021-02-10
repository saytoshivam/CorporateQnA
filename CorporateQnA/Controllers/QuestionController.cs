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
        private readonly IQuestionService QuestionRepository;
        public QuestionController(IQuestionService questionRepository)
        {
            QuestionRepository = questionRepository;
        }
        [HttpPost]
        public void PostQuestion(Question question)
        {
            QuestionRepository.PostQuestion(question);
        }
        [HttpGet("{id}")]
        public IEnumerable<QuestionDetails> GetQuestionsByUserId(int id)
        {
            return QuestionRepository.GetQuestionsByUserId(id);
        }
        public IEnumerable<QuestionDetails> GetQuestionDetails()
        {
            return QuestionRepository.GetQuestionDetails();
        }
        [Route("report/{questionId}")]
        public bool ReportQuestion(int questionId)
        {
            return QuestionRepository.ReportQuestion(questionId);
        }
        [Route("upvote/{questionId}")]
        public bool UpVoteQuestion(int questionId)
        {
            return QuestionRepository.UpVoteQuestion(questionId);
        }
    }
}
