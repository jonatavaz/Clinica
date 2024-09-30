using POJO;

namespace Clinica.Models
{
    public class CreateConsultaViewModel
    {
        public ConsultaModel Consulta { get; set; }
        public List<Medico> Medicos { get; set; }
        public List<Paciente> Pacientes { get; set; }
    }
}
