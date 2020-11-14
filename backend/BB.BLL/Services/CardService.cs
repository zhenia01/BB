using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.DAL.Context;
using AutoMapper;
using BB.Common.Dto;
using Microsoft.EntityFrameworkCore;

namespace BB.BLL.Services
{
    public class CardService : BaseService, ICardService
    {
        public CardService(BBContext context, IMapper mapper) : base(context, mapper) {}

        public async Task<CardDto> GetCardById(int id)
        {
            var card = await Context.Cards.AsNoTracking()
                .FirstAsync(c => c.CardId  == id);

            return Mapper.Map<CardDto>(card);
        }

        public async Task<CardDto> GetCardByNum(string cardNum)
        {
            var card = await Context.Cards.AsNoTracking()
                .FirstAsync(c => c.Number == cardNum);

            return Mapper.Map<CardDto>(card);
        }

        public async Task<ReadOnlyCollection<CardDto>> GetAll()
        {
            var cards = await Context.Cards.AsNoTracking()
                .ToListAsync();

            return Mapper.Map<ReadOnlyCollection<CardDto>>(cards);
        }
    }
}