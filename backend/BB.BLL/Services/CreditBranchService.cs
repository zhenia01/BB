using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BB.BLL.Services
{
    public class CreditBranchService : BaseService, ICreditBranchService
    {
        public static decimal CreditPercent { get; } = 2.5m;

        public CreditBranchService(BBContext context, IMapper mapper) : base(context, mapper)
        {
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