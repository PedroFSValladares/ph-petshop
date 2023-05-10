using Microsoft.AspNetCore.Mvc;
using PHPetshop.Models;
using PHPetshop.Services;
using PHPetshop.Services.Persistence;

namespace PHPetshop.Controllers {

    public class AccountController : Controller{
        public IDbService _context;
        public AccountController(IDbService context) { 
            _context = context;
        }
        public IActionResult Login() { 
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario user) {
            Usuario userBanco = _context.Usuarios.ObterPorEmail(user.Email);
            if(userBanco == null) {
                return NotFound();
            }
            return (user.Senha == userBanco.Senha) ? RedirectToAction("Index", "Home") : BadRequest();
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Usuario user) {
            user.Cargo = Cargo.User;
            _context.Usuarios.Insert(user);
            return RedirectToAction(nameof(Login));
        }
        public IActionResult PasswordRecovery() {
            return View();
        }
    }
}
