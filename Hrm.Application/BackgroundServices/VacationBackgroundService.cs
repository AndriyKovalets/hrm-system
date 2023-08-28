using Microsoft.Extensions.Hosting;

namespace Hrm.Application.BackgroundServices
{
    public class VacationBackgroundService : BackgroundService
    {
        public VacationBackgroundService()
        {
            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}


