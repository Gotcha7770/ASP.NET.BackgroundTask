using System;
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
            //LongRunningTask();
            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                await Task.Delay(2000, token);
                _logger.LogInformation($"Completed at {DateTime.UtcNow.TimeOfDay}");
            });
            return Ok();
        }

        private void LongRunningTask()
        {
            Thread.Sleep(2000);
            _logger.LogInformation("Task completed!");
        }
    }
}