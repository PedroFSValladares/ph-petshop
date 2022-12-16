using Microsoft.AspNetCore.Mvc;
using PHPetshop.Services;

namespace PHPetshop.Controllers {

    public class AccountController : Controller{
        public IActionResult Login() { 
            return View();
        }
        public IActionResult Create() {
            return View();
        }
        public IActionResult PasswordRecovery() {
            return View();
        }
    }
}
