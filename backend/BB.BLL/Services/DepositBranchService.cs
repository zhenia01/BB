using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.Common.Dto.DepositDto;
using BB.DAL.Context;
using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BB.BLL.Services
{
    public class DepositBranchService : BaseService, IDepositBranchService
    {
        private readonly ICheckingBranchService _checkingBranchService;

        public DepositBranchService(ICheckingBranchService checkingBranchService, BBContext context, IMapper mapper) : base(context, mapper)
        {
            _checkingBranchService = checkingBranchService;
        }

        public async Task CreateDepositAccount(int cardId)
        {
            var card = Context.Cards
                .Include(c => c.DepositBranch).DefaultIfEmpty()    
                .Where(c => c.CardId == cardId)
                .SingleAsync();

            if (card.Result.DepositBranchId.HasValue)
            {
                throw new InvalidOperationException("You already have deposit account");
            }

            var depositBranch = new DepositBranch();
            await Context.AddAsync(depositBranch);

            card.Result.DepositBranch = depositBranch;
            
            await Context.SaveChangesAsync();
        }

        public async Task DeleteDepositAccount(int cardId)
        {
            var depositCard = await Context.DepositBranches
                .Include(c => c.Card)
                .Where(c => c.Card.CardId == cardId)
                .SingleAsync();

            
            if (depositCard.Deposits != null)
            {
                throw new InvalidOperationException("You have unpaid deposit");
            }

            /*
            foreach (var deposit in depositCard.Deposits)
            {
                Context.Remove(deposit);
            }
            */

            Context.Remove(depositCard);

            await Context.SaveChangesAsync();
            // чи точно нам потрібно видаляти його 
        }

        public async Task Deposit(DepositDto deposit)
        {
            if (deposit.DepSum <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(deposit.DepSum), "Amount should be positive");
            }
            
            var card = await Context.DepositBranches
                .Include(c => c.Card)
                .Include(c => c.Card.CheckingBranch)
                .Where(c=> c.Card.CardId == deposit.CardId)
                .SingleAsync();

            if (!card.Card.DepositBranchId.HasValue)
            {
                throw new InvalidOperationException("First you need to create Deposit account");
            }

            if (card.Card.CheckingBranch.Balance < deposit.DepSum)
            {
                throw new InvalidOperationException("Not enough money");
            }

            card.Card.CheckingBranch.Balance -= deposit.DepSum;

            var dep = Mapper.Map<Deposit>(deposit);

            dep.DepositBranchId = card.DepositBranchId;

            dep.Percent = dep.Term switch
            {
                1 => 8.0,
                2 => 8.0,
                
                3 => 9.0,
                4 => 9.0,
                5 => 9.0,
                
                6 => 10.0,
                7 => 10.0,
                8 => 10.0,
                
                9 => 10.5,
                10 => 10.5,
                11 => 10.5,
                
                12 => 11.0,
                _ => dep.Percent
            };
            
            
            await Context.AddAsync(dep);
            await Context.SaveChangesAsync();
        }

        public async Task RewardDepositors(CancellationToken stoppingToken)
        {
            var cards = await Context.Cards
                .Include(d => d.DepositBranch)
                .ThenInclude(d => d.Deposits)
                .Include(d => d.CheckingBranch)
                .Include(d => d.CreditBranch)
                .ToListAsync(stoppingToken);


            foreach (var card in cards)
            {
                foreach (var deposit in card.DepositBranch.Deposits)
                {
                    if (deposit.Term <= 0)
                    {
                        await _checkingBranchService.TopUp(card.CardId, deposit.DepSum);

                        Context.Remove(deposit);
                    }
                    else
                    {
                        if (!deposit.PaymentsToDeposit)
                        {
                            if (deposit.CanBeTerminated)
                            {
                                await _checkingBranchService.TopUp(card.CardId,
                                    (deposit.DepSum * ((decimal) (deposit.Percent - 0.5)) / 100));
                            }
                            else
                            {
                                await _checkingBranchService.TopUp(card.CardId,
                                    (deposit.DepSum * ((decimal) deposit.Percent) / 100));
                            }
                        }
                        else
                        {
                            if (deposit.CanBeTerminated)
                            {
                                deposit.DepSum += (deposit.DepSum * ((decimal) (deposit.Percent - 0.5)) / 100);
                            }
                            else
                            {
                                deposit.DepSum += (deposit.DepSum * ((decimal) deposit.Percent) / 100);
                            }
                        }
                    }

                    deposit.Term -= 1;
                }
            }
            await Context.SaveChangesAsync(stoppingToken);
        }
    }
}