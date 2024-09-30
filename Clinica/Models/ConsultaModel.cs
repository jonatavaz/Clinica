using POJO;

namespace Clinica.Models
{
    public class ConsultaModel : Consulta
    {
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
    }
}
