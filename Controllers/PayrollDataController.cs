using Microsoft.AspNetCore.Mvc;

namespace Demo1.Controllers
{
    public class PayrollDataController : Controller
    {
        public IActionResult PayrollData()
        {
            return View();
        }
    }
}
