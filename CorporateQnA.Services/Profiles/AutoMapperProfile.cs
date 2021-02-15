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

            CreateMap<Data.StoredProcedureModels.AnswerDetails, AnswerDetails>()
                .ForMember(dest => dest.TotalLikes, opt => opt.MapFrom(
                  src => JsonConvert.DeserializeObject<List<int>>(src.LikedBy).Count))
                .ForMember(dest => dest.TotalDislikes, opt => opt.MapFrom(
                  src => JsonConvert.DeserializeObject<List<int>>(src.DislikedBy).Count));

            CreateMap<Data.QuestionDetails, QuestionDetails>()
            .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(
                src => JsonConvert.DeserializeObject<List<int>>(src.ViewedBy).Count))
            .ForMember(dest => dest.UpVoteCount, opt => opt.MapFrom(
                src => JsonConvert.DeserializeObject<List<int>>(src.VotedBy).Count));

            CreateMap<Data.UserDetails,UserDetails>()
                 .ForMember(dest => dest.Likes, opt => opt.MapFrom(
                src => JsonConvert.DeserializeObject<List<int>>(src.LikedBy).Count))
            .ForMember(dest => dest.DisLikes, opt => opt.MapFrom(
                src => JsonConvert.DeserializeObject<List<int>>(src.DislikedBy).Count));
        }
    }
}