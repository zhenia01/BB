using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
                .FirstOrDefaultAsync();

            return Mapper.Map<BalanceDto>(card);
        }

        public async Task Withdraw(int cardId, decimal amount)
        {
            PositiveAmount(amount);

            var card = await Context.Cards
                .Include(c => c.CheckingBranch)
                .Include(c => c.CreditBranch).DefaultIfEmpty()
                .Where(c => c.CardId == cardId)
                .FirstOrDefaultAsync();

            var checkingBalance = card.CheckingBranch.Balance;

            if (checkingBalance >= amount)
            {
                card.CheckingBranch.Balance -= amount;
            }
            else
            {
                var creditBalance = card.CreditBranch.Balance;

                if (creditBalance >= amount - checkingBalance)
                {
                    card.CheckingBranch.Balance = 0;
                    card.CreditBranch.Balance -= amount - checkingBalance;
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
            PositiveAmount(amount);

            var card = await (Context.Cards
                .Include(c => c.CheckingBranch)
                .Include(c => c.CreditBranch).DefaultIfEmpty()
                .Where(c => c.CardId == cardId)
                .FirstOrDefaultAsync());

            card.CreditBranch.Balance += amount;
            // UpdateCredit(card);

            await Context.SaveChangesAsync();
        }

        public async Task Transfer(int cardId, string targetCardNum, decimal amount)
        {
            PositiveAmount(amount);

            var cardSrc = await (Context.Cards
                .Include(c => c.CheckingBranch)
                .Include(c => c.CreditBranch).DefaultIfEmpty()
                .Where(c => c.CardId == cardId)
                .FirstOrDefaultAsync());

            var cardDest = await (Context.Cards
                    .Include(c => c.CheckingBranch))
                .Where(c => c.Number == targetCardNum)
                .FirstOrDefaultAsync();

            if (cardSrc.Number == cardDest.Number)
            {
                throw new ArgumentOutOfRangeException(nameof(targetCardNum), "You can't to transfer to the same card");
            }

            cardSrc.CheckingBranch.Balance -= amount;
            cardDest.CheckingBranch.Balance += amount;

            // CorrectBalance(cardSrc);
            //
            // UpdateCredit(cardSrc);
            // UpdateCredit(cardDest);

            await Context.SaveChangesAsync();
        }

        private static void PositiveAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount should be positive");
            }
        }
    }
}