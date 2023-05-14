using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PHPetshop.Controllers {
    public class PetsController : Controller {
        public IActionResult Index() {
            return View();
        }

        [Authorize("admin")]
        public IActionResult Cadastrar() {
            return View();
        }

    }
}
