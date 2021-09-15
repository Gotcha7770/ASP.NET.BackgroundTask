using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP.NET.BackgroundTask.Controllers
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
            LongRunningTask();
            return Ok();
        }

        private void LongRunningTask()
        {
            Thread.Sleep(2000);
            _logger.LogInformation("Task completed!");
        }
    }
}