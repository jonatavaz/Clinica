using POJO;
using Microsoft.AspNetCore.Mvc;
using Clinica.Models;
using Clinica.ViewModels;
using BLL;

namespace Clinica.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly MedicoBLL medicoBLL;
        private readonly ConsultaBLL consultaBLL;
        private readonly PacienteBLL pacienteBLL;

        public ConsultasController()
        {
            medicoBLL = new MedicoBLL();
            consultaBLL = new ConsultaBLL();
            pacienteBLL = new PacienteBLL(); 
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateConsultaModal(int id = 0)
        {
            var viewModel = new CreateConsultaViewModel();

            // Edição
            if (id > 0)
            {
                var consulta = consultaBLL.GetConsultaById(id); 
                if (consulta != null)
                {
                    viewModel = new CreateConsultaViewModel
                    {
                        Consulta = new ConsultaModel
                        {
                            Id = consulta.Id,
                            DataHora = consulta.DataHora,
                            MedicoId = consulta.MedicoId,
                            PacienteId = consulta.PacienteId,
                            Confirmada = consulta.Confirmada,
                            Email = consulta.Email,
                            UsuarioId = consulta.UsuarioId
                        },
                        Medicos = medicoBLL.GetMedicos(),
                        //Pacientes = consultaBLL.GetPacientes() // Assumindo que você tenha esse método
                    };
                }
            }
            // Novo
            else
            {
                viewModel = new CreateConsultaViewModel
                {
                    Consulta = new ConsultaModel(),
                    Medicos = medicoBLL.GetMedicos(),
                    Pacientes = consultaBLL.GetPacientes() // Assumindo que você tenha esse método
                };
            }

            return PartialView("~/Views/Consultas/partials/_modalCreateConsultas.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult ListConsulta()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");

            if (string.IsNullOrEmpty(usuarioId))
            {
                return RedirectToAction("Login");
            }

            var consultasDoUsuario = consultaBLL.GetConsultasPorUsuario(usuarioId);


            return PartialView("~/Views/Consultas/partials/_gridConsulta.cshtml", consultasDoUsuario);
        }

        [HttpPost]
        public JsonResult CreateConsulta(int PacienteId, int MedicoId, string Email, DateTime DataHora)
        {
            var usuarioId = ViewBag.UsuarioId?.ToString();

            var consulta = new Consulta
            {
                PacienteId = PacienteId,
                MedicoId = MedicoId,                
                DataHora = DataHora,
            };

            var resul = consultaBLL.CreateConsulta(consulta); 

            return Json(new { status = resul });
        }

        [HttpPost]
        public JsonResult UpdateConsulta(int id, int PacienteId, int MedicoId, string Email, DateTime DataHora)
        {
            var consulta = consultaBLL.GetConsultaById(id);
            if (consulta != null)
            {
                consulta.PacienteId = PacienteId;
                consulta.MedicoId = MedicoId;
                consulta.Email = Email;
                consulta.DataHora = DataHora;

                consultaBLL.UpdateConsulta(consulta); 

                return Json(new { status = true });
            }

            return Json(new { status = false });
        }

        [HttpPost]
        public JsonResult DeletConsulta(int id)
        {
            var consulta = consultaBLL.GetConsultaById(id);
            if (consulta != null)
            {
                consultaBLL.DeleteConsulta(id); 
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }
    }
}
