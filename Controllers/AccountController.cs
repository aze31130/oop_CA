using Microsoft.AspNetCore.Mvc;

namespace oop_CA.Controllers
{
    /*
     * This controller is here just to handle 
     * if a user tries to access a method without
     * enough permissions
     */
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return BadRequest(new { message = "You do not have access to this !"});
        }
    }
}
