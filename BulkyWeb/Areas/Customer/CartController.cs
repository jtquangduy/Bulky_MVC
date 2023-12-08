using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Customer
{
    [Area("Customer")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
