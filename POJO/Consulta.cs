namespace POJO
{
    public class Consulta : Pessoa
    {
        public int ConsultaId { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime DataHora { get; set; }
        public bool ConsultaConfirmada {  get; set; }
        public int MedicoId => Medico?.MedicoId ?? 0;
        public int PacienteId => Paciente?.PacienteId ?? 0;

    }
}