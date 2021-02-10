using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Interfaces
{
    public interface ICategoryService
    {
        public void PostCategory(Category category);
        public IEnumerable<CategoryDetails> GetCategoryDetails();
    }
}
