using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    class CategoryService:ICategoryService
    {
        private readonly IDbConnection Db;
        private readonly IMapper Mapper;
        public CategoryService(IDbConnectionService conn, IMapper mapper)
        {
            Db = conn.GetDbConnection();
            Mapper = mapper;
        }
        public void PostCategory(Category category)
        {
            category.CreatedOn = DateTime.Now;
            Db.Insert(Mapper.Map<Data.Category>(category));
        }

        public IEnumerable<CategoryDetails> GetCategoryDetails()
        {
           return Mapper.Map<IEnumerable<CategoryDetails>>(Db.GetAll<Data.CategoryDetails>());
        }
    }
}
