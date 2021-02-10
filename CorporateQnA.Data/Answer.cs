using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Data
{
    [Table("Answers")]
    public class Answer
    {
        public int QuestionId { get; set; }

        [Column("Answer")]
        public string QuestionsAnswer { get; set; }

        public int AnsweredBy { get; set; }

        public DateTime AnsweredOn { get; set; }
    }
}