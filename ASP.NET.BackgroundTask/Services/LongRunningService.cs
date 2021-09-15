using System.Threading;
using System.Threading.Tasks;
using ASP.NET.BackgroundTask.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace ASP.NET.BackgroundTask.Services
{
    public class LongRunningService : BackgroundService
    {
        private readonly BackgroundWorkerQueue _queue;

        public LongRunningService(BackgroundWorkerQueue queue)
        {
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _queue.DequeueAsync(stoppingToken);

                await workItem(stoppingToken);
            }
        }
    }
}