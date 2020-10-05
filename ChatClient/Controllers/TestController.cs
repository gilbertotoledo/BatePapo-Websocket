using Microsoft.AspNetCore.Mvc;

namespace ChatClient.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
