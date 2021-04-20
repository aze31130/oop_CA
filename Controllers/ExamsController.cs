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



    }
}
