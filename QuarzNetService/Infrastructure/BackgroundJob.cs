using System;
using System.Threading.Tasks;
using Quartz;

namespace QuarzNetService.Infrastructure
{
    public class BackgroundJob : IJob
    {
        private readonly Func<Task> _factory;

        public BackgroundJob(Func<Task> factory)
        {
            _factory = factory;
        }
        
        public Task Execute(IJobExecutionContext context) => _factory();
    }
}