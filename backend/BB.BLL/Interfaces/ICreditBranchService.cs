using System;
using System.Threading;
using System.Threading.Tasks;
using BB.Common.Dto.DepositDto;

namespace BB.BLL.Interfaces
{
    public interface ICreditBranchService
    {
        Task<CreditBalanceDto> CheckCreditBalance(int cardId);
        Task<bool> CheckExists(int cardId);
        Task PunishForDebts(CancellationToken stoppingToken);
        
        Task CreateCreditAccount(int cardId);
        Task DeleteCreditAccount(int cardId);
    }
}