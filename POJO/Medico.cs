using System.Collections.Generic;

namespace POJO
{
    public class Medico : Especialidade
    {
        public int MedicoId { get; set; }
        public required string Nome { get; set; }
    }
}
