using BLL;
using Clinica.ViewModels;
using Microsoft.AspNetCore.Mvc;
using POJO;
using System.Collections.Generic;
using System.Linq;

namespace Clinica.Controllers
{
    public class LoginController : Controller
    {
        private readonly MedicoBLL _medicoBLL;
        private readonly ConsultaBLL _consultaBLL;
        private readonly PacienteBLL _pacienteBLL;
        private readonly LoginBLL _loginBLL;

        public LoginController()
        {
            _medicoBLL = new MedicoBLL();
            _consultaBLL = new ConsultaBLL();
            _pacienteBLL = new PacienteBLL();
            _loginBLL = new LoginBLL();
        }

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
            if (model == null)
            {
                return Json(new { success = false });
            }

            var login = new Login
            {
                Email = model.Email,
                Senha = model.Senha 
            };

            var usuarioAutenticado = _loginBLL.Authenticate(login);

            if (usuarioAutenticado != null)
            {
                HttpContext.Session.SetString("UsuarioId", usuarioAutenticado.LoginId.ToString());
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "E-mail ou senha inválidos." });
        }

        [HttpPost]
        public IActionResult CreateAccount(RegisterViewModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Modelo inválido." });
            }

            if (string.IsNullOrEmpty(model.Senha))
                return Json(new { success = false, message = "Verifique a senha." });

            var novoLogin = new Login
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                DataNascimento = model.DataNascimento
            };

            var resultado = _loginBLL.AddLogin(novoLogin);

            if (resultado)
            {
                return RedirectToAction("Login");
            }

            return Json(new { success = false, message = "Erro ao criar conta." });
        }
    }
}