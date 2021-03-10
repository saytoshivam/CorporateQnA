using CorporateQnA.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class Filter
    {
        public string Id { get; set; }

        public string ColumnName { get; set; }

        public filterType Type { get; set; }

        public bool IsNullCheck { get; set; }

    }
}
