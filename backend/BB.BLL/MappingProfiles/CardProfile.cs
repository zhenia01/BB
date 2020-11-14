using AutoMapper;
using BB.Common.Dto;
using BB.DAL.Entities;

namespace BB.BLL.MappingProfiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardDto>();
            CreateMap<CardDto, Card>();
            CreateMap<Card, BalanceDto>()
                .ForMember(b => b.CheckingBalance, opt => opt.MapFrom(card => card.CheckingBranch.Balance))
                .ForMember(b => b.CreditBalance, opt => opt.MapFrom(card => card != null ? card.CreditBranch.Balance : 0));
        }
    }
}