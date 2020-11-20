using AutoMapper;
using BB.Common.Dto;
using BB.Common.Dto.Card;
using BB.DAL.Entities;

namespace BB.BLL.MappingProfiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardDto>();
            CreateMap<CardCredentialsDto, Card>();
        }
    }
}