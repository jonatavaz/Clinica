using Clinica.ViewModels;
using Microsoft.AspNetCore.Mvc;
using POJO;

namespace Clinica.Controllers
{
    public class LoginController : Controller
    {
        private static List<Paciente> _usuarios = new List<Paciente>();
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public IActionResult Authenticate(LoginViewModel model)
        {
            if (model is not null)
            {
                Console.WriteLine($"Tentando logar: {model.Email}");

                var usuario = _usuarios.FirstOrDefault(u => u.Email == model.Email);

                if (usuario != null)
                {
                    Console.WriteLine($"Usuário encontrado: {usuario.Email}");
                    if (usuario.Senha == model.Senha)
                    {
                        HttpContext.Session.SetString("UsuarioId", usuario.Email);
                        return Json(new { success = true });
                    }
                    else
                    {
                        Console.WriteLine("Senha incorreta.");
                    }
                }
                else
                {
                    Console.WriteLine("Usuário não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Modelo inválido.");
            }

            return Json(new { success = false, message = "E-mail ou senha inválidos." });
        }

        [HttpPost]
        public IActionResult CreateAccount(RegisterViewModel model)
       {            
            if (model is not null)
            {
                if (string.IsNullOrEmpty(model.Senha))
                    return Json(new { success = false, msg = "Verifique a senha" });

                if (_usuarios.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", "E-mail já cadastrado.");
                    return View(model);
                }

                var novoUsuario = new Paciente
                {
                    Id = _usuarios.Count > 0 ? _usuarios.Max(u => u.Id) + 1 : 1,
                    Nome = model.Email,
                    Email = model.Email,
                    Senha = model.Senha
                };

                _usuarios.Add(novoUsuario);
                Console.WriteLine($"Usuário registrado: {novoUsuario.Email}"); // Verifica logggggggggggggggggggggggggggg

                
                return RedirectToAction("Login");
            }

            
            return View(model);
        }


    }
}
