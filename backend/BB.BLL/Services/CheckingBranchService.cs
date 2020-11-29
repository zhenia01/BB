using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.Common.Dto.Balance;
using BB.DAL.Context;
using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BB.BLL.Services
{
    public class CheckingBranchService : BaseService, ICheckingBranchService
    {
        public CheckingBranchService(BBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<BalanceDto> CheckBalance(int cardId)
        {
            var card = await Context.Cards.AsNoTracking()
                .Include(c => c.CheckingBranch)
                .Include(c => c.CreditBranch).DefaultIfEmpty()
                .Where(c => c.CardId == cardId)
                .SingleAsync();

            return Mapper.Map<BalanceDto>(card);
        }

        public async Task Withdraw(int cardId, decimal amount)
        {
            CheckForPositiveAmount(amount);

            var card = await Context.Cards
                .Include(c => c.CheckingBranch)
                .Include(c => c.CreditBranch).DefaultIfEmpty()
                .Where(c => c.CardId == cardId)
                .SingleAsync();

            if (card.CheckingBranch.Balance >= amount)
            {
                card.CheckingBranch.Balance -= amount;
            }
            else
            {
                var diff = amount - card.CheckingBranch.Balance;

                if (card.CheckingBranch.Balance >= diff)
                {
                    card.CheckingBranch.Balance = 0;
                    card.CheckingBranch.Balance -= diff;
                }
                else
                {
                    throw new InvalidOperationException("Not enough money");
                }
            }

            await Context.SaveChangesAsync();
        }

        public async Task TopUp(int cardId, decimal amount)
        {
            CheckForPositiveAmount(amount);

            var card = await Context.Cards
                .Include(c => c.CheckingBranch)
                .Include(c => c.CreditBranch).DefaultIfEmpty()
                .Where(c => c.CardId == cardId)
                .SingleAsync();

            card.CheckingBranch.Balance += amount;
            CheckCredit(card);

            await Context.SaveChangesAsync();
        }

        private static void CheckCredit(Card card)
        {
            if (card.CreditBranch != null)
            {
                if (card.CreditBranch.Balance < card.CreditBranch.Available)
                {
                    var diff = card.CreditBranch.Available - card.CreditBranch.Balance;
                    if (diff < card.CheckingBranch.Balance)
                    {
                        card.CreditBranch.Balance = card.CreditBranch.Available;
                        card.CheckingBranch.Balance -= diff;
                    }
                    else
                    {
                        diff = card.CheckingBranch.Balance;
                        card.CreditBranch.Balance += diff;
                        card.CheckingBranch.Balance = 0;
                    }
                }
            }
        }

        public async Task Transfer(int sourceCardId, string targetCardNum, decimal amount)
        {
            CheckForPositiveAmount(amount);

            var sourceCard = await Context.Cards
                .Include(c => c.CheckingBranch)
                .Include(c => c.CreditBranch).DefaultIfEmpty()
                .Where(c => c.CardId == sourceCardId)
                .SingleAsync();

            var cardDest = await Context.Cards
                .Include(c => c.CheckingBranch)
                .Include(c => c.CreditBranch).DefaultIfEmpty()
                .Where(c => c.Number == targetCardNum)
                .SingleAsync();

            if (sourceCard.Number == targetCardNum)
            {
                throw new InvalidOperationException("You can't transfer to the same card");
            }

            if (sourceCard.CheckingBranch.Balance >= amount)
            {
                sourceCard.CheckingBranch.Balance -= amount;
                cardDest.CheckingBranch.Balance += amount;
            }
            else
            {
                if (sourceCard.CreditBranch == null ||
                    sourceCard.CreditBranch.Balance + sourceCard.CreditBranch.Balance < amount)
                {
                    throw new InvalidOperationException("Not enough money");
                }

                var diff = amount - sourceCard.CheckingBranch.Balance;

                sourceCard.CheckingBranch.Balance = 0;
                sourceCard.CreditBranch.Balance -= diff;
                await TopUp(cardDest.CardId, amount);
            }

            CheckCredit(cardDest);
            await Context.SaveChangesAsync();
        }

        private static void CheckForPositiveAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount should be positive");
            }
        }
    }
}