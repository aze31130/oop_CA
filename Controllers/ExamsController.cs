using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;

namespace oop_CA.Controllers
{
    public class ExamsController : Controller
    {
        private readonly Context _context;
        public ExamsController(Context context)
        {
            _context = context;
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Remove()
        {
            return View();
        }
    }
}
