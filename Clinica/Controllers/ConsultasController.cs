using POJO;
using Microsoft.AspNetCore.Mvc;
using Clinica.Models;
using Clinica.ViewModels;

namespace Clinica.Controllers
{
    public class ConsultasController : Controller
    {
        private static List<ConsultaModel> _consultas = new List<ConsultaModel>();
        private static List<Medico> _medicos = new List<Medico>
        {
            new Medico { Id = 1, Nome = "Dr. João", NomeEspecialidade = "Cardiologia" },
            new Medico { Id = 2, Nome = "Dra. Maria", NomeEspecialidade = "Pediatria" }
        };
        private static List<Paciente> _pacientes = new List<Paciente>
        {
            new Paciente { Id = 1, Nome = "Maicon", Email = "Maicon@exemplo.com" },
            new Paciente { Id = 2, Nome = "Ana", Email = "ana@example.com" }
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(LoginViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                if (model.UsuarioId == "admin" && model.Senha == "senha") 
                {
                    // Armazenar o ID do usuário na sessão
                    HttpContext.Session.SetString("UsuarioId", model.UsuarioId);
                    return RedirectToAction("Index", "Consultas");
                }
                ModelState.AddModelError("", "Usuário ou senha inválidos.");
            }
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult CreateConsultaModal(int id = 0)
        {
            var viewModel = new CreateConsultaViewModel();
            if (id > 0)
            {
                var consulta = _consultas.FirstOrDefault(c => c.Id == id);
                if (consulta != null)
                {
                    viewModel = new CreateConsultaViewModel
                    {
                        Consulta = consulta,
                        Medicos = _medicos,
                        Pacientes = _pacientes
                    };
                }
            }
            else
            {
                viewModel = new CreateConsultaViewModel
                {
                    Consulta = new ConsultaModel(),
                    Medicos = _medicos,
                    Pacientes = _pacientes
                };
            }
            
            return PartialView("~/Views/Consultas/partials/_modalCreateConsultas.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var consulta = _consultas.FirstOrDefault(c => c.Id == id);
            if (consulta != null)
            {
                var viewModel = new CreateConsultaViewModel
                {
                    Consulta = consulta,
                    Medicos = _medicos,
                    Pacientes = _pacientes
                };

                return PartialView("~/Views/Consultas/partials/_editConsulta.cshtml", viewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public JsonResult CreateConsulta(int PacienteId, int MedicoId, string Email, DateTime DataHora)
        {
            var usuarioId = ViewBag.UsuarioId?.ToString();

            var consulta = new ConsultaModel
            {
                Id = _consultas.Count > 0 ? _consultas.Max(c => c.Id) + 1 : 1,
                PacienteId = PacienteId,
                MedicoId = MedicoId,
                Email = Email,
                DataHora = DataHora,
                Confirmada = true,
                UsuarioId = usuarioId
            };

            _consultas.Add(consulta);

            var consultasDoUsuario = _consultas
                .Where(c => c.UsuarioId == usuarioId)
                .Select(c => new
                {
                    c.DataHora,
                    MedicoNome = _medicos.FirstOrDefault(m => m.Id == c.MedicoId)?.Nome,
                    PacienteNome = _pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Nome,
                    c.Confirmada
                })
                .ToList();

            if (consultasDoUsuario?.Count > 0)            
                return Json(new { status = true, model = consultasDoUsuario, nome = consultasDoUsuario.First().PacienteNome });
            
            else            
                return Json(new { status = false });
        }

        [HttpGet]
        public IActionResult ListConsulta(string usuarioId)
       {
            //List<ConsultaModel> consultasDoUsuario = new List<ConsultaModel>();

            var consultasDoUsuario = _consultas
                .Where(c => c.UsuarioId == usuarioId)
                .Select(c => new ConsultaModel
                {
                    Id = c.Id,
                    DataHora = c.DataHora,
                    MedicoId = c.MedicoId,
                    PacienteId = c.PacienteId,
                    Confirmada = c.Confirmada,
                    Email = c.Email,
                    Medico = _medicos.FirstOrDefault(m => m.Id == c.MedicoId), 
                    Paciente = _pacientes.FirstOrDefault(p => p.Id == c.PacienteId)
                })
                .ToList();

            return PartialView("~/Views/Consultas/partials/_gridConsulta.cshtml", consultasDoUsuario);
        }

        [HttpPost]
        public JsonResult UpdateConsulta(int id, int PacienteId, int MedicoId, string Email, DateTime DataHora)
        {
            var consulta = _consultas.FirstOrDefault(c => c.Id == id);
            if (consulta != null)
            {
                consulta.PacienteId = PacienteId;
                consulta.MedicoId = MedicoId;
                consulta.Email = Email;
                consulta.DataHora = DataHora;

                return Json(new { status = true });
            }

            return Json(new { status = false });
        }


        
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var consulta = _consultas.FirstOrDefault(c => c.Id == id);
            if (consulta != null)
            {
                _consultas.Remove(consulta);
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }
    }
}
