using Microsoft.AspNetCore.Mvc;

namespace JoshWWarren.Controllers
{
    [Route("api/[controller]")]
    public class SoftwareVersionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Search")]
        public IActionResult Search(string? searchTerm)
        {
            return Ok(SoftwareManager.Search(searchTerm));
        }
    }
}