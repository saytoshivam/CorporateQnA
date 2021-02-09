using CorporateQnA.Models;
using CorporateQnA.Data;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace CorporateQnA.Services
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Answer, Models.Answer>().ReverseMap();
            CreateMap<Data.ApplicationUser, Models.ApplicationUser>().ReverseMap();
            CreateMap<Data.Category, Models.Category>().ReverseMap();
            CreateMap<Data.CategoryDetails, Models.CategoryDetails>().ReverseMap();
            CreateMap<Data.Question, Models.Question>().ReverseMap();
            CreateMap<Data.QuestionDetails, Models.QuestionDetails>().ReverseMap();
            CreateMap<Data.Answer, Models.Answer>().ReverseMap();
            CreateMap<Data.UserDetails, Models.UserDetails>().ReverseMap();
        }
    }
}