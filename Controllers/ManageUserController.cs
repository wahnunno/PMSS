using Demo1.con_db;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.Controllers
{
    public class ManageUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUserForm()
        {
            return View();
        }

    }
}
