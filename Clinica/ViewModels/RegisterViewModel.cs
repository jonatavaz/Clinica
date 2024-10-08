using POJO;

namespace Clinica.ViewModels
{
    public class RegisterViewModel : Consulta
    {
        
        public string Email { get; set; }
                
        public string Senha { get; set; }
                
        public string ConfirmarSenha { get; set; }

        public string TipoUsuario { get; set; }
    }

}
