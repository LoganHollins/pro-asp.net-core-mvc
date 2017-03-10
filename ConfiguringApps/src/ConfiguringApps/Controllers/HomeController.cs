using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConfiguringApps.Infrastructure;
using Microsoft.Extensions.Logging;

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        private UptimeService uptime;
        private ILogger logger;

        public HomeController(UptimeService uptimeService, ILogger<HomeController> log)
        {
            uptime = uptimeService;
            logger = log;
        }

        public IActionResult Index(bool throwException = false) {

            if (throwException)
            {
                throw new System.NullReferenceException();
            }

            logger.LogDebug($"Handled {Request.Path} at uptime {uptime.Uptime}");
            return  View(new Dictionary<string, string>
            {
                ["Message"] = "This is the Index action",
                ["Uptime"] = $"{uptime.Uptime}ms"
            });

        }
        
        public ViewResult Error()
        {
            return View("Index", new Dictionary<string, string>
            {
                ["Message"] = "This is the Error action"
            });
        }
    }
}
