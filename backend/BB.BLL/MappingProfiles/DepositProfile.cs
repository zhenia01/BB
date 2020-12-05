using AutoMapper;
using BB.Common.Dto.DepositDto;
using BB.DAL.Entities;

namespace BB.BLL.MappingProfiles
{
    public class DepositProfile : Profile
    {
        public DepositProfile()
        {
            CreateMap<DepositDto, Deposit>();
        }
    }
}