using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POJO
{
    public class Login : Pessoa
    {
        public int LoginId { get; set; }
        public DateTime DataHora { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }

    }
}
