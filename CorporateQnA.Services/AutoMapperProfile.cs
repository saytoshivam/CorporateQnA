using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, Models.Core.ApplicationUser>().ReverseMap();
            CreateMap<Category, Models.Core.Category>().ReverseMap();
            CreateMap<Question, Models.Core.Question>().ReverseMap();
            CreateMap<UserDetails, Models.Core.UserDetails>().ReverseMap();
            CreateMap<QuestionDetails, Models.Core.QuestionDetails>().ReverseMap();
            CreateMap<AnswerDetails, Models.Core.AnswerDetails>().ReverseMap();
        }
    }
}
