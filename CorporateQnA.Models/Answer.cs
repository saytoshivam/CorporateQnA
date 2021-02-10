﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class Answer
    {
        public int QuestionId { get; set; }

        public string QuestionsAnswer { get; set; }

        public int AnsweredBy { get; set; }

        public DateTime AnsweredOn { get; set; }
    }
}