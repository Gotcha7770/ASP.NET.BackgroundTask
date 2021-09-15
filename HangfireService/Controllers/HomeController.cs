using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HangfireService.Controllers
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
            BackgroundJob.Enqueue(() => LongRunningTask(CancellationToken.None));
            return Ok();
        }
        
        [NonAction] //проблема - нельзя вызывать приватные методы!
        public async Task LongRunningTask(CancellationToken token)
        {
            await Task.Delay(2000, token);
            _logger.LogInformation("Task completed!");
        }
    }
}