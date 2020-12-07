using System;
using System.Threading;
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BB.Blazor.HostedServices
{
    public class DebtBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        
        public DebtBackgroundService(IServiceProvider services)
        {
            _services = services;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckDebt(stoppingToken);
                    
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (OperationCanceledException) {}
            }
        }

        private async Task CheckDebt(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();
            
            var service = scope.ServiceProvider.GetRequiredService<ICreditBranchService>();

            await service.PunishForDebts(stoppingToken);
        }
    }
}