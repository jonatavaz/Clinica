using POJO;

namespace Clinica.ViewModels
{
    public class LoginViewModel : Consulta
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public Pessoa pessoa { get; set; }
    }
}
