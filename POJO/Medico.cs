using System.Collections.Generic;


namespace POJO
{
    public class Medico : Pessoa
    {
        public int MedicoId { get; set; }

        public string NomeEspecialidade { get; set; }
        public Especialidade Especialidade { get; set; }

    }
}
