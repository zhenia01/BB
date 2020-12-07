using System;
using System.Threading;
using System.Threading.Tasks;
using BB.Common.Dto.DepositDto;

namespace BB.BLL.Interfaces
{
    public interface IDepositBranchService
    {
        Task Deposit(DepositDto deposit);

        Task<bool> CheckExists(int cardId);

        Task CreateDepositAccount(int cardId);
        Task DeleteDepositAccount(int cardId);

        Task RewardDepositors(CancellationToken stoppingToken);
    }
}