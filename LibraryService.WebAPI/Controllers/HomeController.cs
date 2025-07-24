using Microsoft.AspNetCore.Mvc;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult GetRoot()
        {
            // Return a 200 OK status with the desired string message
            return Ok("Backend works");
        }
    }
}