using POJO;

namespace Clinica.Interfaces
{
    public interface IMedico 
    {
        string Nome { get; set; }
        int Idade { get; set; }
        string NomeEspecialidade { get; set; }
        void RealizarConsulta();
    }
}
