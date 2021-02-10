using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class QuestionDetails
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public DateTime AskedOn { get; set; }

        public int AskedBy { get; set; }

        public string UserImage { get; set; }

        public int ViewCount { get; set; }

        public int UpVoteCount  { get; set; }

        public int AnswerCount { get; set; }

        public bool IsResolved { get; set; }
    }
}