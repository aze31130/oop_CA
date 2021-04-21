using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using System.Linq;
using static oop_CA.Utils.MarksUtils;

namespace oop_CA.Controllers
{
    public class MarksController : Controller
    {
        private readonly Context _context;
        public MarksController(Context context)
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
            int studentId = 2;
            return View(getMarks(studentId, _context.marks.ToList()));
        }

        public IActionResult Remove()
        {
            return View();
        }
    }
}
