using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NonORMExample.Infrastructure;
using NonORMExample.Models;

namespace NonORMExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CategoryRepository _repository;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, IWebHostEnvironment env)
        {
            _logger = logger;
            var conn = config.GetConnectionString("DefaultConnection");
            conn = conn.Replace("%DATAPATH%", env.ContentRootPath);

            _repository = new CategoryRepository(conn);
        }

        public IActionResult Index()
        {
            return View(new CategoryViewModel
            {
                CategoryList = _repository.GetAll().ToList()
            });
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
