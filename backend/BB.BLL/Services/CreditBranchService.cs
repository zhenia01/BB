using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.DAL.Context;
using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BB.BLL.Services
{
    public class CreditBranchService : BaseService, ICreditBranchService
    {
        public static decimal CreditAvailable { get; } = 500;
        public static decimal CreditPercent { get; } = 2.5m;

        public CreditBranchService(BBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task CreateCreditAccount(int cardId)
        {
            var card = await Context.Cards
                .Include(c => c.CreditBranch).DefaultIfEmpty()
                .Where(c => c.CardId == cardId)
                .SingleAsync();

            if (card.DepositBranchId.HasValue)
            {
                throw new InvalidOperationException("You already have credit account");
            }

            var creditBranch = new CreditBranch()
            {
                Available = CreditAvailable,
                Balance = CreditAvailable
            };

            await Context.AddAsync(creditBranch);
            
            card.CreditBranch = creditBranch;

            await Context.SaveChangesAsync();
        }

        public async Task DeleteCreditAccount(int cardId)
        {
            var creditBranch = await Context.CreditBranches
                .Include(c => c.Card)
                .Where(c => c.Card.CardId == cardId)
                .SingleAsync();

            
            if (creditBranch.Balance < creditBranch.Available || creditBranch.Debt < 0)
            {
                throw new InvalidOperationException("Repay the debt first");
            }

            Context.Remove(creditBranch);

            await Context.SaveChangesAsync();
        }

        public async Task PunishForDebts(CancellationToken stoppingToken)
        {
            var creditBranches = await Context.CreditBranches
                .Where(c => c.Balance < c.Available)
                .ToListAsync(stoppingToken);

            foreach (var card in creditBranches)
            {
                var diff = card.Available - card.Balance;
                card.Debt += decimal.Multiply(Math.Abs(diff), CreditPercent) / 100;
            }

            await Context.SaveChangesAsync(stoppingToken);
            
        }
    }
}