using Microsoft.AspNetCore.Mvc;

namespace YoutubeService.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return RedirectPermanent("/swagger");
        }
    }
}