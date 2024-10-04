using POJO;

namespace Clinica.Models
{
    public class ConsultaModel : Consulta
    {       
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }

        //extra:

        public string NomePaciente { get; set; }
        public string NomeMedico { get; set; }
        public string EmailPaciente { get; set; }

    }
}
