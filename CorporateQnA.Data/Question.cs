using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Data
{
    [Table("Questions")]
    public class Question
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int AskedBy { get; set; }

        public DateTime AskedOn { get; set; }
    }
}