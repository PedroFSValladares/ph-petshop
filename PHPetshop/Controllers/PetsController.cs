﻿using Microsoft.AspNetCore.Mvc;

namespace PHPetshop.Controllers {
    public class PetsController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Cadastrar() {
            return View();
        }

    }
}
