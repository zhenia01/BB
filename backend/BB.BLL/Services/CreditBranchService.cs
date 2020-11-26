using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.DAL.Context;

namespace BB.BLL.Services
{
    public class CreditBranchService : BaseService, ICreditBranchService
    {
        public CreditBranchService(BBContext context, IMapper mapper) : base(context, mapper)
        {
            
        }
        
        public async Task PunishForDebts(CancellationToken stoppingToken)
        {
            var debtors = Context.CreditBranches
                .Where(cb => cb.Balance < cb.Available && cb.WithdrawTime.HasValue && (cb.WithdrawTime.Value - DateTime.Now).Seconds )
        }
    }
}