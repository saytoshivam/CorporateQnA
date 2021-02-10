using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Data.StoredProcedureModels
{
    public class AnswerDetails
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string UserImage { get; set; }

        public string Answer { get; set; }

        public DateTime AnsweredOn { get; set; }

        public bool IsBestSolution { get; set; }

        public string LikedBy { get; set; }

        public string DislikedBy { get; set; }
    }
}