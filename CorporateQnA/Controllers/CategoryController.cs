using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ICategoryService CategoryService;
        public CategoryController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [Authorize]
        [HttpPost("{category}")]
        public void PostCategory(Category category)
        {
            CategoryService.PostCategory(category);
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<CategoryDetails> GetCategoryDetails()
        {
            return CategoryService.GetCategoryDetails();
        }
    }
}
