using System.Threading;
using System.Threading.Tasks;
using CustomBackgroundService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomBackgroundService.Controllers
{
    [Controller]
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;
        private readonly ILogger<HomeController> _logger;

        public HomeController(BackgroundWorkerQueue backgroundWorkerQueue, ILogger<HomeController> logger)
        {
            _backgroundWorkerQueue = backgroundWorkerQueue;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _backgroundWorkerQueue.QueueBackgroundWorkItem(LongRunningTask);
            return Ok();
        }

        private async Task LongRunningTask(CancellationToken token)
        {
            await Task.Delay(2000, token);
            _logger.LogInformation("Task completed!");
        }
    }
}