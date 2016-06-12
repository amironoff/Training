using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RealWorldAspNetCore.Controllers
{
    public class StatusCodeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public StatusCodeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: /<controller>/
        public IActionResult Index(int statusCode)
        {
            var reExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            _logger.LogInformation("Unexpected StatusCode: {statusCode}, OriginalPath: {OriginalPath}", statusCode, reExecuteFeature.OriginalPath);
            return View();
        }
    }
}
