using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using POJO;

namespace Clinica.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
