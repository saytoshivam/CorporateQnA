﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
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