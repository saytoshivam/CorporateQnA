using System;

namespace CorporateQnA.Models.Core
{
    public class Question
    {
        public int Id { get; set; }
        public string Head { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int AskedBy { get; set; }
        public DateTime AskedOn { get; set; } = DateTime.Now;
    }
}