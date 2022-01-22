using Book_Shop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book_Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var errorViewModel = ErrorViewModelBuilder.BuildError(id);

            if (errorViewModel == null) return StatusCode(500);
            
            return View("Error", errorViewModel);
        }
    }
}