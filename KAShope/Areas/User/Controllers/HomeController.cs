using System.Diagnostics;
using KAShope.Data;
using KAShope.Models;
using Microsoft.AspNetCore.Mvc;

namespace KAShope.User.Controllers
{
    [Area("user")]
    public class HomeController : Controller
    {

     
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext context = new ApplicationDbContext();   

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var cats = context.Categories.ToList(); 
            //ViewData["categories"] = context.Categories.ToList();
            ViewBag.Categories = context.Categories.ToList();
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
