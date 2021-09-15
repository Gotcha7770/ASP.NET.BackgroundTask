using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomBackgroundService.Interfaces
{
    public interface IBackgroundTaskQueue
    {
        ValueTask EnqueueAsync(Func<CancellationToken, ValueTask> workItem);

        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);
    }
}