using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CorporateQnA.Data
{
    public class ApplicationUser:IdentityUser<int>
    {
        public string FullName { get; set; }

        public string UserImage { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string JobLocation { get; set; }
    }
}