using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class AnswerDetails
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Answer { get; set; }

        public string UserImage { get; set; }

        public DateTime AnsweredOn { get; set; }

        public int TotalLikes { get; set; }

        public int TotalDislikes { get; set; }

        public bool IsBestSolution { get; set; }
    }
}