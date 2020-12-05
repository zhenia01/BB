using System;
using System.Threading;
using System.Threading.Tasks;
using BB.Common.Dto.DepositDto;

namespace BB.BLL.Interfaces
{
    public interface IDepositBranchService
    {
        Task Deposit(DepositDto deposit);

        Task RewardDepositors(CancellationToken stoppingToken);
    }
}