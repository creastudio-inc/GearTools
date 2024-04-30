using AOEntityFramework.Repository;
using AOEntityFrameworkDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AOEntityFrameworkDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Product> _productRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<Product> productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            Product pr = new Product();
            pr.FieldValues.Count();
            pr.Name = "aa";
            pr.FieldValues.Count();

            return View();
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
