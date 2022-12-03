using Microsoft.AspNetCore.Mvc;

namespace PHPetshop.Controllers {
    public class ServicosController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
