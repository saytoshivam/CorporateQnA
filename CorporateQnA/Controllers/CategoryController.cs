using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateQnA.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService CategoryRepository;
        public CategoryController(ICategoryService categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        [HttpPost]
        public void PostCategory(Category category)
        {
            CategoryRepository.PostCategory(category);
        }
       
        [HttpGet]
        public IEnumerable<CategoryDetails> GetCategoryDetails()
        {
            return CategoryRepository.GetCategoryDetails();
        }
    }
}
