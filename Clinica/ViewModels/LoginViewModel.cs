using POJO;

namespace Clinica.ViewModels
{
    public class LoginViewModel : Consulta
    {
        public string UsuarioId { get; set; }
        public string Senha { get; set; }
    }
}

