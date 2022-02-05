using Book_Shop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Web.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("error/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var errorViewModel = ErrorViewModelBuilder.BuildError(id);

            if (errorViewModel == null) return StatusCode(500);

            return View("Error", errorViewModel);
        }
    }
}
