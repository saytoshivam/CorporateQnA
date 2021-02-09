using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Data
{
    [Table("UsersView")]
    public class UserDetails
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string UserImage { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string JobLocation { get; set; }

        public int QuestionsAsked { get; set; }

        public int QuestionsAnswered { get; set; }

        public int QuestionsSolved { get; set; }

        public string LikedBy { get; set; }

        public string DislikedBy { get; set; }
    }
}