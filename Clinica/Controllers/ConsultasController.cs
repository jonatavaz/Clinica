using Microsoft.AspNetCore.Mvc;
using Clinica.Models;
using Clinica.ViewModels;
using BLL;
using POJO;

namespace Clinica.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly MedicoBLL _medicoBLL;
        private readonly ConsultaBLL _consultaBLL;
        private readonly PacienteBLL _pacienteBLL;
        private readonly LoginBLL _loginBLL;

        
        public ConsultasController()
        {
            _medicoBLL = new MedicoBLL();
            _consultaBLL = new ConsultaBLL();
            _pacienteBLL = new PacienteBLL();
            _loginBLL = new LoginBLL();
        }

        
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateConsultaModal(int id = 0)
        {
            var viewModel = new CreateConsultaViewModel
            {
                Consulta = new ConsultaModel(),
                Medicos = _medicoBLL.GetAllMedicos() ?? new List<Medico>(),
                Pacientes = _pacienteBLL.GetAllPacientes() ?? new List<Paciente>() 
            };

            if (id > 0)
            {
                var consulta = _consultaBLL.GetConsultaById(id);
                if (consulta != null)
                {
                    viewModel.Consulta = new ConsultaModel
                    {
                        ConsultaId = consulta.ConsultaId,
                        DataHora = consulta.DataHora,
                        Medico = new Medico { MedicoId = consulta.MedicoId },
                        Paciente = new Paciente { PacienteId = consulta.PacienteId },
                        Email = consulta.Email
                    };
                }
                else
                {
                    return Json(new { status = false });
                }
            }

            return PartialView("~/Views/Consultas/partials/_modalCreateConsultas.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult ListConsulta()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");

            if (string.IsNullOrEmpty(usuarioId))            
                return RedirectToAction("Login", "Login");            

            var consultasDoUsuario = _consultaBLL.GetConsultasPorUsuario(usuarioId);

            var model = new List<ConsultaModel>();

            return PartialView("~/Views/Consultas/partials/_gridConsulta.cshtml", model);
        }


        [HttpPost]
        public JsonResult CreateConsulta(int pacienteId, int medicoId, string email, DateTime dataHora)
        {
            var usuarioId = ViewBag.UsuarioId?.ToString();

            var consulta = new Consulta
            {
                Paciente = new Paciente { PacienteId = pacienteId },
                Medico = new Medico { MedicoId = medicoId },
                DataHora = dataHora,
                ConsultaConfirmada = false,
                Email = email 
            };

            var resultado = _consultaBLL.CreateConsulta(consulta);

            return Json(new { status = resultado });
        }

        
        [HttpPost]
        public JsonResult UpdateConsulta(int ConsultaId, int pacienteId, int medicoId, string email, DateTime dataHora)
        {
            var consulta = _consultaBLL.GetConsultaById(ConsultaId);
            if (consulta != null)
            {
                consulta.Paciente = new Paciente {PacienteId = pacienteId};
                consulta.Medico = new Medico { MedicoId = medicoId };
                consulta.Email = email;
                consulta.DataHora = dataHora;

                var resultado = _consultaBLL.UpdateConsulta(consulta);

                return Json(new { status = resultado });
            }

            return Json(new { status = false });
        }

        
        [HttpPost]
        public JsonResult DeleteConsulta(int ConsultaId)
        {
            var consultaExistente = _consultaBLL.GetConsultaById(ConsultaId);
            if (consultaExistente != null)
            {
                var resultado = _consultaBLL.DeleteConsulta(ConsultaId);
                return Json(new { status = resultado });
            }

            return Json(new { status = false });
        }
    }
}