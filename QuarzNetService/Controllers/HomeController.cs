using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace QuarzNetService.Controllers
{
    [Controller]
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //_backgroundWorkerQueue.QueueBackgroundWorkItem(LongRunningTask);
            return Ok();
        }

        private async Task LongRunningTask(CancellationToken token)
        {
            await Task.Delay(2000, token);
            _logger.LogInformation("Task completed!");
        }
    }
}