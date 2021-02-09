﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models.Core
{
    public class ApplicationUser
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string ProfileImage { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string JobLocation { get; set; }
    }
}
