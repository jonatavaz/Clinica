namespace POJO
{
    public class Consulta : Pessoa
    {
        public int ConsultaId { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime DataHora { get; set; }
        public bool ConsultaConfirmada { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }


    }
}