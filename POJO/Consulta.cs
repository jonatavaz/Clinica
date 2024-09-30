namespace POJO
{
    public class Consulta
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataHora { get; set; }
        public string Email {  get; set; }
        public bool Confirmada { get; set; } = true;
       //public bool Notificada { get; set; } = false;
        public string UsuarioId { get; set; }

    }
}