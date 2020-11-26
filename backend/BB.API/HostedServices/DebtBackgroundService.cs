using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BB.API.HostedServices
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
            await Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {

                        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    }
                    catch (OperationCanceledException) {}
                }
            }, stoppingToken);
        }

        private async Task CheckDebt(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();
            
            // var context = scope.ServiceProvider.GetRequiredService<BBC>()
        }
    }
}