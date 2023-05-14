using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PHPetshop.Models;
using PHPetshop.Services.Mail;
using PHPetshop.Services.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PHPetshop.Controllers {

    public class AccountController : Controller{
        public readonly DbService _context;
        public readonly ILogger<AccountController> _logger;
        public readonly PasswordHasher<Usuario> _passwordHasher;
        public readonly MailClient _mailer;

        public AccountController(DbService context, PasswordHasher<Usuario> passwordHasher, MailClient mailer, ILogger<AccountController> logger) {
            _passwordHasher = passwordHasher;
            _context = context;
            _logger = logger;
            _mailer = mailer;
        }

        public async Task<IActionResult> Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario user, [FromForm]string remember) {
            Usuario userBanco = _context.Usuarios.ObterPorEmail(user.Email);
            if(userBanco == null) {
                ViewData["ErrorMessage"] = "Não há conta registrada com esse email.";
                return View();
            }

            var result = _passwordHasher.VerifyHashedPassword(user, userBanco.Senha, user.Senha);
            if(result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded) {
                /*
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(@"8JM8MyZp7YaO7R3Cp+6dNSR5Q4puL7GgMd/NOz+IWEM=");
                var header = Response.Headers.Authorization.FirstOrDefault();
                var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("login", userBanco.Email),
                        new Claim("Nome", userBanco.Nome),
                        new Claim("cargo", userBanco.Cargo.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var encryptedToken = tokenHandler.WriteToken(token);
                */
                var claims = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Email, userBanco.Email),
                        new Claim(ClaimTypes.Name, userBanco.Nome),
                        new Claim(ClaimTypes.Role, userBanco.Cargo.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties {
                    RedirectUri = "/Home/Index",
                    IsPersistent = remember == "true",
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims), authProperties);

                _logger.LogInformation($"[{DateTime.UtcNow}]: Usuario {user.Email} logou no sistema.");

                return LocalRedirect(authProperties.RedirectUri);
            }
            ViewData["ErrorMessage"] = "Usuario ou senha inválidos.";
            return View();
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            var userEmail = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
            _logger.LogInformation($"[{DateTime.UtcNow}]: Usuario {userEmail} deslogou do sistema");
            return LocalRedirect("/");
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario user) {
            string hashedPassword = _passwordHasher.HashPassword(user, user.Senha);
            user.Senha = hashedPassword;
            user.Cargo = Cargo.User;
            _context.Usuarios.Insert(user);
            ViewData["EmailConfirmationInfo"] = new EmailConfirmationInfo {
                EmailConfirmed = false,
                UserEmail = user.Email
            };
            return RedirectToAction(nameof(EmailConfirmation));
        }

        public IActionResult EmailConfirmation() {
            return View();
        }

        [HttpGet("/ConfirmEmail/{token}")]
        public IActionResult ConfirmEmail(string token) {               //faça duas actions diferentes
            EmailConfirmationInfo info = new EmailConfirmationInfo {    //uma para o aviso e outra para confirmar
                EmailConfirmed = true,                                  //os dados virão pelo token no link
                UserEmail = "teste"
            };
            Console.WriteLine("Email Confirmado");
            return RedirectToAction(nameof(EmailConfirmation), info);
        }

        public IActionResult PasswordRecovery() {
            return View();
        }
    }
}
