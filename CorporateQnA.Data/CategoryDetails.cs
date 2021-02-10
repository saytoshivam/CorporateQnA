using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CorporateQnA.Data
{
    [Table("CategoriesView")]
    public class CategoryDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TotalTags { get; set; }

        public int TagsThisWeek { get; set; }

        public int TagsThisMonth { get; set; }
    }
}