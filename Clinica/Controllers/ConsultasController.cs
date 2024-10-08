using Microsoft.AspNetCore.Mvc;
using Clinica.Models;
using Clinica.ViewModels;
using BLL;
using POJO;
using DAL;

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
                Pacientes = _pacienteBLL.GetAllPacientes() ?? new List<Paciente>(),
                Paciente = new Paciente() 
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
                        MedicoId = consulta.MedicoId,
                        PacienteId = consulta.PacienteId,
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

            foreach (var consulta in consultasDoUsuario)
            {
                model.Add(new ConsultaModel
                {
                    ConsultaId = consulta.ConsultaId,
                    DataHora = consulta.DataHora,
                    Medico = consulta.Medico, 
                    Paciente = consulta.Paciente,
                    NomeMedico = consulta.NomeMedico,
                    NomePaciente = consulta.NomePaciente,
                    EmailPaciente = consulta.EmailPaciente
                });
            }
            return PartialView("~/Views/Consultas/partials/_gridConsulta.cshtml", model);
        }

        [HttpPost]
        public JsonResult CreateConsulta(string pacienteNome, int medicoId, string email, DateTime? dataNascimento, DateTime dataHora)
        {
            var usuarioId = ViewBag.UsuarioId?.ToString();

            
            if (string.IsNullOrWhiteSpace(pacienteNome) || medicoId <= 0 || string.IsNullOrWhiteSpace(email) || dataHora == default)
            {
                return Json(new { status = false, message = "Dados inválidos." });
            }

            
            if (!IsHorarioDisponivel(dataHora, medicoId))            
                return Json(new { status = false, message = "O horário selecionado não está disponível." });
            

            var pacienteDAL = new PacienteDAL();
            var pacientes = pacienteDAL.GetPacientesPorNome(pacienteNome);

            int pacienteId;

            if (pacientes == null || !pacientes.Any())
            {
                var novoPaciente = new Paciente
                {
                    Nome = pacienteNome,
                    Email = email,
                    Senha = "",
                    DataNascimento = dataNascimento ?? DateTime.Now
                };

                pacienteId = _pacienteBLL.AddPaciente(novoPaciente);
            }
            else
            {
                pacienteId = pacientes.First().PacienteId;
            }

            var consulta = new Consulta
            {
                Paciente = new Paciente { PacienteId = pacienteId },
                Medico = new Medico { MedicoId = medicoId },
                Email = email,
                DataHora = dataHora,
                ConsultaConfirmada = true
            };

            var resultado = _consultaBLL.CreateConsulta(consulta);

            return Json(new { status = resultado });
        }

        
        private bool IsHorarioDisponivel(DateTime dataHora, int medicoId)
        {
            var consultasExistentes = _consultaBLL.GetConsultasPorMedicoEData(medicoId, dataHora);
            return !consultasExistentes.Any(); 
        }


        [HttpPost]
        public JsonResult EditConsulta(int consultaId, int pacienteId, int medicoId, DateTime dataHora)
        {
            try
            {
                if (consultaId <= 0 || medicoId <= 0 || dataHora == default)
                {
                    return Json(new { status = false, message = "Dados inválidos." });
                }

                var consulta = _consultaBLL.GetConsultaById(consultaId);
                if (consulta != null)
                {
                    consulta.Paciente = new Paciente { PacienteId = pacienteId };
                    consulta.Medico = new Medico { MedicoId = medicoId };
                    consulta.DataHora = dataHora;

                    var resultado = _consultaBLL.UpdateConsulta(consulta);
                    return Json(new { status = resultado });
                }

                return Json(new { status = false, message = "Consulta não encontrada." });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Ocorreu um erro: " + ex.Message });
            }
        }


        [HttpPost]
        public JsonResult DeleteConsulta(int consultaId)
        {
            try
            {
                if (consultaId <= 0)
                {
                    return Json(new { status = false });
                }

                var consulta = _consultaBLL.GetConsultaById(consultaId);
                if (consulta != null)
                {
                    var resultado = _consultaBLL.DeleteConsulta(consultaId);
                    if (resultado)
                    {
                        return Json(new { status = true });
                    }
                    else
                    {
                        return Json(new { status = false });
                    }
                }
                return Json(new { status = false });
            }
            catch (Exception ex)
            { 
                return Json(new { status = false });
            }
        }
    }
}