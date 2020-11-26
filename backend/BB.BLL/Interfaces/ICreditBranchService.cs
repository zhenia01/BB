using System.Threading;
using System.Threading.Tasks;

namespace BB.BLL.Interfaces
{
    public interface ICreditBranchService
    {
        Task PunishForDebts(CancellationToken stoppingToken);
    }
}