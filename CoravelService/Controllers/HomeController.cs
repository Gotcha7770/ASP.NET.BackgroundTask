using System.Threading;
using System.Threading.Tasks;
using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoravelService.Controllers
{
    [Controller]
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IQueue _queue;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IQueue queue, ILogger<HomeController> logger)
        {
            _queue = queue;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _queue.QueueAsyncTask(() => LongRunningTask(CancellationToken.None));
            return Ok();
        }
        
        private async Task LongRunningTask(CancellationToken token)
        {
            await Task.Delay(2000, token);
            _logger.LogInformation("Task completed!");
        }
    }
}