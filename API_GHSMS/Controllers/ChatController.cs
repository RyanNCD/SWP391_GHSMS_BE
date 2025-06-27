using Microsoft.AspNetCore.Mvc;

namespace API_GHSMS.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
