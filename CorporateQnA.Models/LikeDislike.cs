using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models.Core
{
    public class LikeDislike
    {
        public IEnumerable<int> LikedBy { get; set; }

        public IEnumerable<int> DisLikedBy { get; set; }
    }
}
