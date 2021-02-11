using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;

namespace CorporateQnA.Services
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Answer, Answer>().ReverseMap();
            CreateMap<Data.ApplicationUser, ApplicationUser>().ReverseMap();
            CreateMap<Data.Category, Category>().ReverseMap();
            CreateMap<Data.CategoryDetails, CategoryDetails>().ReverseMap();
            CreateMap<Data.Question, Question>().ReverseMap();
            CreateMap<Data.Answer, Answer>().ReverseMap();
            CreateMap<Data.UserDetails, UserDetails>().ReverseMap();

            //CreateMap<QuestionDetails, Data.QuestionDetails>()
            //.ForMember(dest => dest.AskedBy, opt => opt.MapFrom(
            //    src => JsonConvert.SerializeObject(src.AskedBy)));
        }
    }
}