using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.Common.Dto.DepositDto;
using BB.Common.Dto.User;
using BB.DAL.Context;
using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BB.BLL.Services
{
    public class DepositBranchService : BaseService, IDepositBranchService
    {
        public static TimeSpan DepositTimeLapse = new TimeSpan(0, 0, 5);

        private readonly ICheckingBranchService _checkingBranchService;

        public DepositBranchService(ICheckingBranchService checkingBranchService, BBContext context, IMapper mapper) : base(context, mapper)
        {
            _checkingBranchService = checkingBranchService;
        }
        
        public async Task Deposit(DepositDto deposit)
        {
            if (deposit.DepSum <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(deposit.DepSum), "Amount should be positive");
            }
            
            var card = await Context.Cards
                .Include(c => c.DepositBranch)
                .Include(c => c.CheckingBranch)
                .Where(c=> c.DepositBranchId == deposit.DepositBranchId)
                .SingleAsync();

            if (card.CheckingBranch.Balance < deposit.DepSum)
            {
                throw new InvalidOperationException("Not enough money");
            }

            card.CheckingBranch.Balance -= deposit.DepSum;

            var dep = Mapper.Map<Deposit>(deposit);

            switch (dep.Term)
            {
                case 1 :
                case 2 :
                    dep.Percent = 8.0;
                    break;
                case 3 :
                case 4 :
                case 5 :
                    dep.Percent = 9.0;
                    break;
                case 6 :
                case 7 :
                case 8 :
                    dep.Percent = 10.0;
                    break;
                case 9 :
                case 10 :
                case 11 :
                    dep.Percent = 10.5;
                    break;
                case 12 :
                    dep.Percent = 11.0;
                    break;
            }

            await Context.AddAsync(dep);
            await Context.SaveChangesAsync();
        }

        public async Task RewardDepositors(DateTime currDate, CancellationToken stoppingToken)
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