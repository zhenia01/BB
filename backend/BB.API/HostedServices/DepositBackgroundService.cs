using System;
using System.Threading;
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.BLL.Services;
using BB.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BB.API.HostedServices
{
    public class DepositBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public DepositBackgroundService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckRewards(stoppingToken);

                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
        }

        private async Task CheckRewards(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IDepositBranchService>();

            await service.RewardDepositors(stoppingToken);
        }
    }
}