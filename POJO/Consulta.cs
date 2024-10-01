namespace POJO
{
    public class Consulta
    {
        public int ConsultaId { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataHora { get; set; }
        public string EmailPaciente {  get; set; }
        //public bool Confirmada { get; set; } = true;
        //public string UsuarioId { get; set; }
        //public string Senha { get; set; }
        //public string ConfirmarSenha { get; set; }

    }
}