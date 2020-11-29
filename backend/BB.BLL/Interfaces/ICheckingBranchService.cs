using System.Threading.Tasks;
using BB.Common.Dto.Balance;

namespace BB.BLL.Interfaces
{
    public interface ICheckingBranchService
    {
        Task<BalanceDto> CheckBalance(int cardId);
        Task Withdraw(int cardId, decimal amount);
        Task TopUp(int cardId, decimal amount);
        Task Transfer(int sourceCardId, string targetCardNum, decimal amount);
    }
}