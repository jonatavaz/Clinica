using BLL;
using Clinica.ViewModels;
using Microsoft.AspNetCore.Mvc;
using POJO;
using System.Collections.Generic;

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
            if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Senha))
            {
                return Json(new { success = false, message = "E-mail e senha são obrigatórios." });
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

            
            if (string.IsNullOrEmpty(model.TipoUsuario))
                return Json(new { success = false, message = "Selecione um tipo de usuário." });

            var novoLogin = new Login
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                DataNascimento = model.DataNascimento
            };

            bool isPaciente = model.TipoUsuario == "Paciente";
            var resultado = _loginBLL.AddLogin(novoLogin, isPaciente);

            if (resultado)
            {
                return RedirectToAction("Index");
            }

            return Json(new { success = false, message = "Erro ao criar conta." });
        }
    }
}
