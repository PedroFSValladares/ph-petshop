using Microsoft.AspNetCore.Mvc;

namespace PHPetshop.Controllers {
    public class ProdutosController : Controller{
        public IActionResult Index() {
            return View();
        }
    }
}
